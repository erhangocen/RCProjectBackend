using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IFavoriteService
    {
        IDataResult<List<Favorite>> GetAll();
        IDataResult<List<Favorite>> GetByUserId(int id);
        IDataResult<List<FavoriteDto>> GetAllDetails();
        IDataResult<List<FavoriteDto>> GetAllDetailsByUserId(int id);
        IDataResult<Favorite> GetById(int id);
        IResult Add(Favorite favorite);
        IResult Update(Favorite favorite);
        IResult Delete(Favorite favorite);
         
    }
}
