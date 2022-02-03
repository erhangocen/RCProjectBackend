using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class FavoriteManager : IFavoriteService
    {
        private IFavoriteDal _favoriteDal;

        public FavoriteManager(IFavoriteDal favoriteDal)
        {
            _favoriteDal = favoriteDal;
        }

        [CacheRemoveAspect("IFavoriteService.GetAllDetailsByUserId")]
        public IResult Add(Favorite favorite)
        {
            _favoriteDal.Add(favorite);
            return new SuccessResult(Messages.FavoriteAdd);
        }

        [CacheRemoveAspect("IFavoriteService.GetAllDetailsByUserId")]
        public IResult Delete(Favorite favorite)
        {
            _favoriteDal.Delete(favorite);
            return new SuccessResult(Messages.FavoriteDelete);
        }

        public IDataResult<List<Favorite>> GetAll()
        {
            return new SuccessDataResult<List<Favorite>>(_favoriteDal.GetAll());
        }

        public IDataResult<List<FavoriteDto>> GetAllDetails()
        {
            return new SuccessDataResult<List<FavoriteDto>>(_favoriteDal.GetAllDetails());
        }

        [CacheAspect]
        public IDataResult<List<FavoriteDto>> GetAllDetailsByUserId(int id)
        {
            return new SuccessDataResult<List<FavoriteDto>>(_favoriteDal.GetAllDetails(p => p.UserId == id));
        }

        public IDataResult<Favorite> GetById(int id)
        {
            return new SuccessDataResult<Favorite>(_favoriteDal.Get(p => p.Id == id));
        }

        public IDataResult<List<Favorite>> GetByUserId(int id)
        {
            return new SuccessDataResult<List<Favorite>>(_favoriteDal.GetAll(p => p.UserId == id));
        }

        [CacheRemoveAspect("IFavoriteService.GetAllDetailsByUserId")]
        public IResult Update(Favorite favorite)
        {
            _favoriteDal.Update(favorite);
            return new SuccessResult(Messages.FavoriteUpdate);
        }
    }
}
