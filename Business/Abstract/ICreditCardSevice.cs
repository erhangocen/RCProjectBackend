using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICreditCardSevice
    {      
        IDataResult<CreditCard> GetById(int id);

        IDataResult<List<CreditCard>> GetAllByUserId(int userId);

        IResult Add(CreditCard creditCard);
    }
}
