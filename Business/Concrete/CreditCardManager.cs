using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardSevice
    {
        ICreditCardDal _creditCardDal;
        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        [CacheRemoveAspect("ICreditCardService.GetAllByUserId")]
        [ValidationAspect(typeof(CreditCardValidator))]
        [SecuredOperation("Customer")]
        public IResult Add(CreditCard creditCard)
        {
            _creditCardDal.Add(creditCard);
            return new SuccessResult();
        }

        [CacheRemoveAspect("ICreditCardService.GetAllByUserId")]
        public IResult Delete(CreditCard creditCard)
        {
            _creditCardDal.Delete(creditCard);
            return new SuccessResult(Messages.CreditCardDelete);
        }

        [CacheAspect]
        [SecuredOperation("Customer")]
        public IDataResult<List<CreditCard>> GetAllByUserId(int userId)
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll(p=>p.UserId == userId));
        }

        [SecuredOperation("Customer")]
        public IDataResult<CreditCard> GetById(int id)
        {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(c => c.Id == id));
        }

        [CacheRemoveAspect("ICreditCardService.GetAllByUserId")]
        public IResult Update(CreditCard creditCard)
        {
            _creditCardDal.Update(creditCard);
            return new SuccessResult(Messages.CreditCardUpdate);
        }
    }
}
