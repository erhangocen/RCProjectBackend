using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ITokenOperationService
    {
        IDataResult<List<TokenOperation>> GetAll();
        IDataResult<TokenOperation> GetById(int id);
        IDataResult<List<TokenOperation>> GetByUserId(int userid);
        IResult Add(TokenOperation tokenOperation);
    }
}
