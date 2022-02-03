using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;
        
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [CacheRemoveAspect("IBrandService.GetAll")]
        public IResult Add(Brand brand)
        {

            IResult result = BusinessRules.Run(CheckOtherBrandNames(brand.BrandName));
            if (result != null)
            {
                return result;
            }

            _brandDal.Add(brand);
            return new SuccessResult(Messages.BrandAdd);
        }

        [CacheRemoveAspect("IBrandService.GetAll")]
        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDelete);
        }

        [CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }

        public IDataResult<Brand> GetByBrandId(int id)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(p => p.BrandId == id));
        }

        [CacheRemoveAspect("IBrandService.GetAll")]
        public IResult Update(Brand brand)
        {

            IResult result = BusinessRules.Run(CheckOtherBrandNames(brand.BrandName));
            if (result != null)
            {
                return result;
            }

            _brandDal.Update(brand);
            return new SuccessResult(Messages.BrandUpdate);
        }

        private IResult CheckOtherBrandNames(string brandName)
        {
            var result = _brandDal.GetAll(b => b.BrandName == brandName).Any();
            if (result)
            {
                return new ErrorResult(Messages.BrandNameAlreadyExist);
            }

            return new SuccessResult();
        }
    }
}
