using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
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
        private IPaymentService _paymentService;

        public SaleManager(ISaleDal saleDal, IPaymentService paymentService)
        {
            _saleDal = saleDal;
            _paymentService = paymentService;
        }

        [CacheRemoveAspect("ISaleService.GetFullSaleDetailsByUserId")]
        [TransactionScopeAspect]
        public IResult Add(Sale sale)
        {
            _paymentService.PayForProduct(sale.UserId, sale.ProductId);
            _saleDal.Add(sale);
            return new SuccessResult(Messages.SaleAdd);
        }

        [CacheRemoveAspect("ISaleService.GetFullSaleDetailsByUserId")]
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
        
        [CacheAspect]
        public IDataResult<List<SaleDto>> GetFullSaleDetailsByUserId(int id)
        {
            return new SuccessDataResult<List<SaleDto>>(_saleDal.GetFullSaleDetails(p => p.UserId == id));
        }

        public IDataResult<List<Sale>> GetSalesByUserId(int id)
        {
            return new SuccessDataResult<List<Sale>>(_saleDal.GetAll(p => p.UserId == id));
        }

        [CacheRemoveAspect("ISaleService.GetFullSaleDetailsByUserId")]
        public IResult Update(Sale sale)
        {
            _saleDal.Update(sale);
            return new SuccessResult(Messages.SaleUpdate);
        }
    }
}
