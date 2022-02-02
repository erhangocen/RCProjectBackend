using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBidService
    {
        IDataResult<List<Bid>> GetAll();
        IDataResult<Bid> GetById(int id);
        IDataResult<List<Bid>> GetByUserId(int id);
        IDataResult<List<Bid>> GetByCategoryId(int id);
        IResult Add(Bid bid);
        IResult Update(Bid bid);
        IResult Delete(Bid bid);
    }
}
