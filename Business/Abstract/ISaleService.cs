using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ISaleService
    {
        IDataResult<List<Sale>> GetAll();
        IDataResult<List<SaleDto>> GetFullSaleDetails();
        IDataResult<List<Sale>> GetSalesByUserId(int id);
        IDataResult<Sale> GetById(int id);
        IResult Add(Sale sale);
        IResult Update(Sale sale);
        IResult Delete(Sale sale);
    }
}
