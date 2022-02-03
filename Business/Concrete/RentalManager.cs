using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
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
        private ITokenService _tokenService;
        private IProductService _productService;
        
        public RentalManager(IRentalDal rentalDal, ITokenService tokenService, IProductService productService)
        {
            _rentalDal = rentalDal;
            _tokenService = tokenService;
            _productService = productService;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(IsRentable(rental));
            if(result != null)
            {
                return result;
            }

            Token t = _tokenService.GetByUserId(rental.UserId).Data;
            Product p = _productService.GetById(rental.ProductId).Data;

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

        public IDataResult<List<Rental>> GetRentalsByUserId(int id)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(p => p.UserId == id));
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdate);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult IsRentable(Rental rental)
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
    }
}
