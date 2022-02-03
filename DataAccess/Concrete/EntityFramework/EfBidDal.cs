using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBidDal : EfEntityRepositoryBase<Bid, RCProjectDBContext>, IBidDal
    {
        public List<BidDto> GetAllBidDetails(Expression<Func<Bid, bool>> filter = null)
        {
            using (RCProjectDBContext context = new RCProjectDBContext())
            {
                var result = from b in filter == null ? context.Bids : context.Bids.Where(filter)
                             join u in context.Users
                             on b.UserId equals u.UserId
                             join brnd in context.Brands
                             on b.BrandId equals brnd.BrandId
                             join c in context.Colors
                             on b.ColorId equals c.ColorId
                             join ct in context.Categories
                             on b.CategoryId equals ct.CategoryId
                             select new BidDto
                             {
                                 BidId = b.BidId,
                                 BidDate = b.BidDate,
                                 BidValue = b.BidValue,
                                 BrandId = b.BrandId,
                                 BrandName = brnd.BrandName,
                                 CategoryName = ct.CategoryName,
                                 CatgoryId = b.CategoryId,
                                 ColorId = b.ColorId,
                                 ColorName = c.ColorName,
                                 Description = b.Description,
                                 Status = b.Status,
                                 Title = b.Title,
                                 UserId = b.UserId,
                                 UserName = u.Username,
                                 UserFullName = u.FirstName + " " + u.LastName
                             };
                return result.ToList();

            }
        }
    }
}
