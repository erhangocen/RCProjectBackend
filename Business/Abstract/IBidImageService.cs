using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBidImageService
    {
        IDataResult<List<BidImage>> GetAll();
        IDataResult<List<BidImage>> GetByBidId(int id);
        IDataResult<BidImage> GetById(int id);
        IResult Add(IFormFile file, BidImage bidImage);
        IResult Update(IFormFile file, BidImage bidImage);
        IResult Delete(BidImage bidImage);
    }
}
