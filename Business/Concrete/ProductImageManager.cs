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
    public class ProductImageManager : IProductImageService
    {
        IProductImageDal _productImageDal;
        public ProductImageManager(IProductImageDal productImageDal)
        {
            _productImageDal = productImageDal;
        }

        [CacheRemoveAspect("IProductImageService.GetById")]
        public IResult Add(IFormFile file, ProductImage productImage)
        {
            IResult result = BusinessRules.Run(CheckImageisFull(productImage));
            if (result != null)
            {
                return result;
            }

            productImage.ImagePath = ProductImageHelper.Add(file);

            var data = productImage.ImagePath.Split('\\').LastOrDefault();
            productImage.ImagePath = "/Images/ProductImages/" + data;

            productImage.Date = DateTime.Now;
            _productImageDal.Add(productImage);
            return new SuccessResult(Messages.AddProductImage);
        }

        [CacheRemoveAspect("IProductImageService.GetById")]
        public IResult Delete(ProductImage productImage)
        {
            var result = BusinessRules.Run(ProductImageDelete(productImage));
            ProductImageHelper.Delete(_productImageDal.Get(p=>p.Id == productImage.Id).ImagePath);
            if (result != null)
            {
                return result;
            }
            _productImageDal.Delete(productImage);
            return new SuccessResult(Messages.UpdateProductImage);
        }

        public IDataResult<List<ProductImage>> GetAll()
        {
            return new SuccessDataResult<List<ProductImage>>(_productImageDal.GetAll());
        }

        public IDataResult<List<ProductImage>> GetByProductId(int id)
        {
            return new SuccessDataResult<List<ProductImage>>(CheckIfProductImageNull(id));
        }

        [CacheAspect]
        public IDataResult<ProductImage> GetById(int id)
        {
            return new SuccessDataResult<ProductImage>(_productImageDal.Get(p => p.Id == id));
        }

        public IResult Update(IFormFile file, ProductImage productImage)
        {
            productImage.ImagePath = ProductImageHelper.Update(_productImageDal.Get(P=>P.Id == productImage.Id).ImagePath, file);

            productImage.Date = DateTime.Now;
            _productImageDal.Update(productImage);
            return new SuccessResult(Messages.DeleteProductImage);
        }

        [CacheRemoveAspect("IProductImageService.GetById")]
        private IResult ProductImageDelete(ProductImage productImage)
        {
            try
            {
                File.Delete(productImage.ImagePath);
            }
            catch (Exception exception)
            {

                return new ErrorResult(exception.Message);
            }

            return new SuccessResult();
        }

        private List<ProductImage> CheckIfProductImageNull(int id)
        {
            string path = @"Images\ProductImages\default.jpg";

            var result = _productImageDal.GetAll(p=>p.Id == id).Any();
            if (!result)
            {
                return new List<ProductImage> { new ProductImage { ProductId = id, ImagePath = path, Date = DateTime.Now } };
            }
            return _productImageDal.GetAll(p => p.Id == id);
        }

        private IResult CheckImageisFull(ProductImage productImage)
        {
            int result = _productImageDal.GetAll(p=>p.ProductId == productImage.ProductId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.ImagesreFull);
            }
            return new SuccessResult();
        }
    }
}
