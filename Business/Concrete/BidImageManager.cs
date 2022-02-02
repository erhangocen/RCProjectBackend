using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BidImageManager : IBidImageService
    {
        private IBidImageDal _bidImageDal;
        
        public BidImageManager(IBidImageDal bidImageDal)
        {
            _bidImageDal = bidImageDal; 
        }

        public IResult Add(IFormFile file, BidImage bidImage)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(BidImage bidImage)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<BidImage>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<BidImage>> GetByBidId(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<BidImage> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(IFormFile file, BidImage bidImage)
        {
            throw new NotImplementedException();
        }
    }
}
