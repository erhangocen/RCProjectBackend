using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Business.BusinessAspects.Autofac;
using Entities.Dtos;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        private ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        //[PerformanceAspect(5)]
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            //Thread.Sleep(5000);
            return new SuccessDataResult<List<Product>>(_productDal.GetAll());
        }

        //[SecuredOperation("Admin")]
        [LogAspect(typeof(FileLogger))]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == categoryId).ToList());
        }


        [ValidationAspect(typeof(ProductValidator), Priority = 1)]
        [CacheRemoveAspect("IProductService.GetAllDetails")]
        [SecuredOperation("Editor,Admin")]
        public IResult Add(Product product)
        {
            //status: active(1), renting(2), deactive(3)

            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.Title));

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [CacheRemoveAspect("IProductService.GetAllDetails")]
        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        [CacheRemoveAspect("IProductService.GetAllDetails")]
        public IResult Update(Product product)
        {

            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Product product)
        {
            _productDal.Update(product);
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        [CacheAspect]
        public IDataResult<List<ProductDto>> GetAllDetails()
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetAllProductDetails());
        }

        public IDataResult<List<ProductDto>> GetAllDetailsByColor(int colorId)
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetAllProductDetails(p => p.ColorId == colorId));
        }

        public IDataResult<List<ProductDto>> GetAllDetailsByBrand(int brandId)
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetAllProductDetails(p => p.BrandId == brandId));
        }

        public IDataResult<List<ProductDto>> GetAllDetailsByCategory(int categoryId)
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetAllProductDetails(p => p.CategoryId == categoryId));
        }

        public IDataResult<List<ProductDto>> GetAllDetailsByColorAndBrand(int colorId, int brandId)
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetAllProductDetails().Where(x => x.BarandId == brandId && x.ColorId == colorId).ToList());
        }

        public IDataResult<List<ProductDto>> GetAllDetailsByBrandAndCategory(int brandId, int categoryId)
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetAllProductDetails().Where(x => x.BarandId == brandId && x.CategoryId == categoryId).ToList());
        }

        public IDataResult<List<ProductDto>> GetAllDetailsByColorAndCategory(int colorId, int categoryId)
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetAllProductDetails().Where(x => x.CategoryId == categoryId && x.ColorId == colorId).ToList());
        }

        public IDataResult<List<ProductDto>> GetAllDetailsByColorCategoryAndBrand(int colorId, int categoryId, int brandId)
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetAllProductDetails().Where(x => x.BarandId == brandId && x.ColorId == colorId && x.CategoryId == categoryId).ToList());
        }

        private IResult CheckIfProductNameExists(string title)
        {

            var result = _productDal.GetAll(p => p.Title == title).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }
    }
}
