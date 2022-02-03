using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal: EfEntityRepositoryBase<Product, RCProjectDBContext>, IProductDal
    {
        public List<ProductDto> GetAllProductDetails(Expression<Func<Product, bool>> filter = null)
        {
            using (RCProjectDBContext context = new RCProjectDBContext())
            {
                var result = from p in filter == null ? context.Products : context.Products.Where(filter)
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             join b in context.Brands
                             on p.BrandId equals b.BrandId
                             join col in context.Colors
                             on p.ColorId equals col.ColorId
                             select new ProductDto()
                             {
                                 ProductId = p.ProductId,
                                 BarandId = p.BrandId,
                                 BrandName = b.BrandName,
                                 CategoryId = c.CategoryId,
                                 CategoryName = c.CategoryName,
                                 ColorId = col.ColorId,
                                 ColorName = col.ColorName,
                                 Title = p.Title,
                                 Description = p.Description,
                                 RentPrice = p.RentPrice,
                                 SalePrice = p.SalePrice,
                                 AddDate = p.AddDate,
                                 ImagePaths = context.ProductImages.Where(p=>p.ProductId == p.Id).ToList(),
                                 Status = p.Status
                             };
                return result.ToList();
            }
        }
    }
}
