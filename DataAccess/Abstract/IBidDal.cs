using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IBidDal : IEntityRepository<Bid>
    {
        List<BidDto> GetAllBidDetails(Expression<Func<Bid, bool>> filter = null);
    }
}
