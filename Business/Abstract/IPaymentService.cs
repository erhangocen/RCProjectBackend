using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IResult PayForProduct(int userId, int productId);
        IResult PayForToken(TokenOperation tokenOperation);
    }
}
