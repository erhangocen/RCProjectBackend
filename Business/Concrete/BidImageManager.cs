using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        [CacheRemoveAspect("IBidImageService.GetAll")]
        public IResult Add(IFormFile file, BidImage bidImage)
        {
            IResult result = BusinessRules.Run(CheckImagesFull(bidImage));
            if (result != null)
            {
                return result;
            }

            bidImage.ImagePath = BidImageHelper.Add(file);

            var data = bidImage.ImagePath.Split('\\').LastOrDefault();
            bidImage.ImagePath = "/Images/BidImages/" + data;

            bidImage.Date = DateTime.Now;
            _bidImageDal.Add(bidImage);
            return new SuccessResult(Messages.AddBidImage);
        }

        [CacheRemoveAspect("IBidImageService.GetAll")]
        public IResult Delete(BidImage bidImage)
        {
            var result = BusinessRules.Run(BidImageDelete(bidImage));
            BidImageHelper.Delete(_bidImageDal.Get(b => b.Id == bidImage.Id).ImagePath);
            if (result != null)
            {
                return result;
            }
            _bidImageDal.Delete(bidImage);
            return new SuccessResult(Messages.UpdateBidImage);
        }

        [CacheAspect]
        public IDataResult<List<BidImage>> GetAll()
        {
            return new SuccessDataResult<List<BidImage>>(_bidImageDal.GetAll());
        }

        public IDataResult<List<BidImage>> GetByBidId(int id)
        {
            return new SuccessDataResult<List<BidImage>>(_bidImageDal.GetAll(p => p.BidId == id));
        }

        public IDataResult<BidImage> GetById(int id)
        {
            return new SuccessDataResult<BidImage>(_bidImageDal.Get(p => p.Id == id));
        }

        [CacheRemoveAspect("IBidImageService.GetAll")]
        public IResult Update(IFormFile file, BidImage bidImage)
        {
            bidImage.ImagePath = BidImageHelper.Update(_bidImageDal.Get(b => b.Id == bidImage.Id).ImagePath, file);

            bidImage.Date = DateTime.Now;
            _bidImageDal.Update(bidImage);
            return new SuccessResult(Messages.DeleteBidImage);
        }

        private IResult BidImageDelete(BidImage bidImage)
        {
            try
            {
                File.Delete(bidImage.ImagePath);
            }
            catch (Exception exception)
            {

                return new ErrorResult(exception.Message);
            }

            return new SuccessResult();
        }

        private IResult CheckImagesFull(BidImage bidImage)
        {
            int result = _bidImageDal.GetAll(b => b.BidId == bidImage.BidId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.ImagesreFull);
            }
            return new SuccessResult();
        }
    }
}
