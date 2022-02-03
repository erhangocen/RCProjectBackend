using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<Product> GetById(int productId);
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetListByCategory(int categoryId);
        IDataResult<List<ProductDto>> GetAllDetails();
        IDataResult<List<ProductDto>> GetAllDetailsByColor(int colorId);
        IDataResult<List<ProductDto>> GetAllDetailsByBrand(int brandId);
        IDataResult<List<ProductDto>> GetAllDetailsByCategory(int categoryId);
        IDataResult<List<ProductDto>> GetAllDetailsByColorAndBrand(int colorId, int brandId);
        IDataResult<List<ProductDto>> GetAllDetailsByBrandAndCategory(int brandId, int categoryId);
        IDataResult<List<ProductDto>> GetAllDetailsByColorAndCategory(int colorId, int categoryId);
        IDataResult<List<ProductDto>> GetAllDetailsByColorCategoryAndBrand(int colorId, int categoryId, int brandId);
        IResult Add(Product product);
        IResult Delete(Product product);
        IResult Update(Product product);
        IResult TransactionalOperation(Product product);

    }
}
