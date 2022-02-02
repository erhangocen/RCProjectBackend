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
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RCProjectDBContext>, IRentalDal
    {
        public bool CheckProductStatus(int productId, DateTime rentDate, DateTime returnDate)
        {
            using (RCProjectDBContext context = new RCProjectDBContext())
            {
                bool checkReturnDateIsNull = context.Set<Rental>().Any(p => p.ProductId == productId && p.ReturnDate == null);
                bool isValidRentDate = context.Set<Rental>().Any(r => r.ProductId == productId && (rentDate >= r.RentDate && rentDate <= r.RentDate) || (rentDate >= r.RentDate && returnDate <= r.ReturnDate) || (r.RentDate >= rentDate && r.RentDate <= returnDate));
                if ((!checkReturnDateIsNull)&&(!isValidRentDate))
                {
                    return true;
                }
                return false;
            }
        }

        public List<RentalDto> GetFullRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (RCProjectDBContext context = new RCProjectDBContext())
            {
                var result = from r in filter == null ? context.Rentals : context.Rentals.Where(filter)
                             join p in context.Products
                             on r.ProductId equals p.ProductId
                             join u in context.Users
                             on r.UserId equals u.UserId
                             select new RentalDto()
                             {
                                 RentalId = r.RentalId,
                                 UserId = u.UserId,
                                 ProductId = p.ProductId,
                                 ProductTitle = p.Title,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 UserName = u.FirstName + u.LastName,
                                 TotalPrice = Convert.ToInt32(((r.RentDate - r.RentDate).TotalDays)*p.RentPrice)
                             };

                return result.ToList();
            }
        }
    }
}
