using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<RentalDto>> GetFullRentalDetails();
        IDataResult<List<RentalDto>> GetFullRentalDetailsByUserId(int id);
        IDataResult<List<Rental>> GetRentalsByUserId(int id);
        IDataResult<Rental> GetRentalById(int id);
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
        IResult CheckProductStatus(Rental rental);
    }
}
