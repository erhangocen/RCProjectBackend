using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IFavoriteDal : IEntityRepository<Favorite>
    {
        List<FavoriteDto> GetAllDetails(Expression<Func<Favorite, bool>> filter = null);
    }
}
