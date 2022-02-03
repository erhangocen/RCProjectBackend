using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;
        private IPaymentService _paymentService;
        
        public RentalManager(IRentalDal rentalDal, IPaymentService paymentService)
        {
            _rentalDal = rentalDal;
            _paymentService = paymentService;
        }

        [CacheRemoveAspect("IRentalService.GetFullRentalDetailsByUserId")]
        [ValidationAspect(typeof(RentalValidator))]
        [TransactionScopeAspect]
        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(IsRentable(rental));
            if(result != null)
            {
                return result;
            }

            _paymentService.PayForProduct(rental.UserId, rental.ProductId);
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdd);
        }

        public IResult CheckProductStatus(Rental rental)
        {
            if (_rentalDal.CheckProductStatus(rental.ProductId, rental.RentDate, rental.ReturnDate))
            {
                return new SuccessResult(Messages.RentalDateOk);
            }
            return new ErrorResult(Messages.RentalReturnDateError);
        }


        [CacheRemoveAspect("IRentalService.GetFullRentalDetailsByUserId")]
        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDelete);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<List<RentalDto>> GetFullRentalDetails()
        {
            return new SuccessDataResult<List<RentalDto>>(_rentalDal.GetFullRentalDetails());
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetRentalsByUserId(int id)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(p => p.UserId == id));
        }

        [CacheRemoveAspect("IRentalService.GetFullRentalDetailsByUserId")]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdate);
        }

        private IResult IsRentable(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.ProductId == rental.ProductId);

            if (result.Any(r => r.ReturnDate >= rental.RentDate))
            {
                return new ErrorResult(Messages.RentalNotAvailable);
            }
            return new SuccessResult();
        }

        public IDataResult<Rental> GetRentalById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(p => p.RentalId == id));
        }

        [CacheAspect]
        public IDataResult<List<RentalDto>> GetFullRentalDetailsByUserId(int id)
        {
            return new SuccessDataResult<List<RentalDto>>(_rentalDal.GetFullRentalDetails(p => p.UserId == id));
        }
    }
}
