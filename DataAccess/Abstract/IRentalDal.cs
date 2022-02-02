using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IRentalDal: IEntityRepository<Rental>
    {
        bool CheckProductStatus(int productId, DateTime rentDate, DateTime returnDate);
        List<RentalDto> GetFullRentalDetails(Expression<Func<Rental, bool>> filter = null);
    }
}
