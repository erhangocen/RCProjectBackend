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
    public class EfSaleDal : EfEntityRepositoryBase<Sale, RCProjectDBContext>, ISaleDal
    {
        public List<SaleDto> GetFullSaleDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (RCProjectDBContext context = new RCProjectDBContext())
            {
                var result = from s in context.Sales
                             join p in context.Products
                             on s.ProductId equals p.ProductId
                             join u in context.Users
                             on s.UserId equals u.UserId
                             select new SaleDto()
                             {
                                 ProductId = p.ProductId,
                                 ProductTitle = p.Title,
                                 Price = p.SalePrice,
                                 SaleId = s.SaleId,
                                 UserId = u.UserId,
                                 UserName = u.FirstName + u.LastName
                             };
                return result.ToList();
            }
        }
    }
}
