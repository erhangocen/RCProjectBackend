using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private ITokenService _tokenService;
        private IProductService _productService;
        private ITokenOperationService _tokenOperationService;

        public PaymentManager(
            ITokenService tokenService, IProductService productService, ITokenOperationService tokenOperationService)
        {
            _tokenService = tokenService;
            _productService = productService;
            _tokenOperationService = tokenOperationService;
        }

        [TransactionScopeAspect]
        public IResult PayForProduct(int userId, int productId)
        {
            //refactor
            Token t = _tokenService.GetByUserId(userId).Data;
            Product p = _productService.GetById(productId).Data;

            if (t.TokenValue < p.SalePrice)
            {
                return new ErrorResult(Messages.InsufficientBalance);
            }

            Token token = new Token
            {
                Id = t.Id,
                UserId = t.UserId,
                TokenValue = t.TokenValue - p.SalePrice
            };

            _tokenService.Update(token);
            return new SuccessResult(Messages.SuccessfullPayment);
        }

        [TransactionScopeAspect]
        public IResult PayForToken(TokenOperation tokenOperation)
        {
            //buraya gerçek satın alma operasyonları eklenicek
            _tokenOperationService.Add(tokenOperation);
            return new SuccessResult();
        }
    }
}
