using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
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
    public class BidManager : IBidService
    {

        private IBidDal _bidDal;

        public BidManager(IBidDal bidDal)
        {
            _bidDal = bidDal;
        }

        [CacheRemoveAspect("IBidService.GetAllBidDetails")]
        [ValidationAspect(typeof(BidValidator))]
        public IResult Add(Bid bid)
        {
            _bidDal.Add(bid);
            return new SuccessResult(Messages.BidAdd);
        }

        [CacheRemoveAspect("IBidService.GetAllBidDetails")]
        public IResult Delete(Bid bid)
        {
            _bidDal.Delete(bid);
            return new SuccessResult(Messages.BidDelete);
        }

        public IDataResult<List<Bid>> GetAll()
        {
            return new SuccessDataResult<List<Bid>>(_bidDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<List<BidDto>> GetAllBidDetails()
        {
            return new SuccessDataResult<List<BidDto>>(_bidDal.GetAllBidDetails());
        }

        public IDataResult<List<BidDto>> GetBidDetailsByUserId(int id)
        {
            return new SuccessDataResult<List<BidDto>>(_bidDal.GetAllBidDetails(p => p.UserId == id));
        }

        public IDataResult<List<Bid>> GetByCategoryId(int id)
        {
            return new SuccessDataResult<List<Bid>>(_bidDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Bid> GetById(int id)
        {
            return new SuccessDataResult<Bid>(_bidDal.Get(p => p.BidId == id));
        }

        public IDataResult<List<Bid>> GetByUserId(int id)
        {
            return new SuccessDataResult<List<Bid>>(_bidDal.GetAll(p => p.UserId == id));
        }

        [CacheRemoveAspect("IBidService.GetAllBidDetails")]
        public IResult Update(Bid bid)
        {
            _bidDal.Update(bid);
            return new SuccessResult(Messages.BidUpdate);
        }

        
        private IResult CheckDailyLimit(string title)
        {
            //burayı doldur
            var result = false; 
            if (result)
            {
                return new ErrorResult(Messages.DailyBidLimitIsFull);
            }

            return new SuccessResult();
        }
    }
}
