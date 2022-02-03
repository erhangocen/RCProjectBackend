using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [CacheRemoveAspect("ICategoryService.GetAll")]
        public IResult Add(Category category)
        {
            IResult result = BusinessRules.Run(CheckOtherCategoryNames(category.CategoryName));
            if(result != null)
            {
                return result;
            }

            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdd);
        }

        [CacheRemoveAspect("ICategoryService.GetAll")]
        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDelete);
        }

        [CacheAspect]
        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll().ToList());
        }

        public IDataResult<Category> GetByCategoryId(int id)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(p => p.CategoryId == id));
        }

        [CacheRemoveAspect("ICategoryService.GetAll")]
        public IResult Update(Category category)
        {

            IResult result = BusinessRules.Run(CheckOtherCategoryNames(category.CategoryName));
            if (result != null)
            {
                return result;
            }

            _categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdate);
        }

        private IResult CheckOtherCategoryNames(string categoryName)
        {
            var result = _categoryDal.GetAll(c=>c.CategoryName == categoryName).Any();
            if (result)
            {
                return new ErrorResult(Messages.CategoryNameAlreadyExist);
            }

            return new SuccessResult();
        }
    }
}
