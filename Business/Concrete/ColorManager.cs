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
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;
        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [CacheRemoveAspect("IColorService.GetAll")]
        public IResult Add(Color color)
        {
            IResult result = BusinessRules.Run(CheckOtherColorNames(color.ColorName));
            if (result != null)
            {
                return result;
            }

            _colorDal.Add(color);
            return new SuccessResult(Messages.ColorAdd);
        }

        [CacheRemoveAspect("IColorService.GetAll")]
        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.ColorDelete);
        }

        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        public IDataResult<Color> GetByColorId(int id)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(p => p.ColorId == id));
        }

        [CacheRemoveAspect("IColorService.GetAll")]
        public IResult Update(Color color)
        {

            IResult result = BusinessRules.Run(CheckOtherColorNames(color.ColorName));
            if (result != null)
            {
                return result;
            }

            _colorDal.Update(color);
            return new SuccessResult(Messages.ColorUpdate);
        }

        private IResult CheckOtherColorNames(string colorName)
        {
            var result = _colorDal.GetAll(c => c.ColorName == colorName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ColorNameAlreadyExist);
            }

            return new SuccessResult();
        }
    }
}
