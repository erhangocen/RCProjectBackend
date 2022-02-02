using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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

        public IResult Add(Bid bid)
        {
            _bidDal.Add(bid);
            return new SuccessResult(Messages.BidAdd);
        }

        public IResult Delete(Bid bid)
        {
            _bidDal.Delete(bid);
            return new SuccessResult(Messages.BidDelete);
        }

        public IDataResult<List<Bid>> GetAll()
        {
            return new SuccessDataResult<List<Bid>>(_bidDal.GetAll());
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

        public IResult Update(Bid bid)
        {
            _bidDal.Update(bid);
            return new SuccessResult(Messages.BidUpdate);
        }
    }
}
