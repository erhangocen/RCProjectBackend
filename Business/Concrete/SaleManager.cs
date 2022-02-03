using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class SaleManager : ISaleService
    {
        private ISaleDal _saleDal;
        private ITokenService _tokenService;
        private IProductService _productService;

        public SaleManager(ISaleDal saleDal, ITokenService tokenService, IProductService productService)
        {
            _saleDal = saleDal;
            _tokenService = tokenService;
            _productService = productService;
        }

        public IResult Add(Sale sale)
        {
            Token t = _tokenService.GetByUserId(sale.UserId).Data;
            Product p = _productService.GetById(sale.ProductId).Data;

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
            _saleDal.Add(sale);
            return new SuccessResult(Messages.SaleAdd);
        }

        public IResult Delete(Sale sale)
        {
            _saleDal.Delete(sale);
            return new SuccessResult(Messages.SaleDelete);
        }

        public IDataResult<List<Sale>> GetAll()
        {
            return new SuccessDataResult<List<Sale>>(_saleDal.GetAll());
        }

        public IDataResult<Sale> GetById(int id)
        {
            return new SuccessDataResult<Sale>(_saleDal.Get(p => p.UserId == id));
        }

        public IDataResult<List<SaleDto>> GetFullSaleDetails()
        {
            return new SuccessDataResult<List<SaleDto>>(_saleDal.GetFullSaleDetails());
        }

        public IDataResult<List<Sale>> GetSalesByUserId(int id)
        {
            return new SuccessDataResult<List<Sale>>(_saleDal.GetAll(p => p.UserId == id));
        }

        public IResult Update(Sale sale)
        {
            _saleDal.Update(sale);
            return new SuccessResult(Messages.SaleUpdate);
        }
    }
}
