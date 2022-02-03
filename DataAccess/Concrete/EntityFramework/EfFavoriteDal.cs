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
    public class EfFavoriteDal : EfEntityRepositoryBase<Favorite, RCProjectDBContext>, IFavoriteDal
    {
        public List<FavoriteDto> GetAllDetails(Expression<Func<Favorite, bool>> filter = null)
        {
            using (RCProjectDBContext context = new RCProjectDBContext())
            {
                var result = from f in filter == null ? context.Favorites : context.Favorites.Where(filter)
                             join u in context.Users
                             on f.UserId equals u.UserId
                             join p in context.Products
                             on f.ProductId equals p.ProductId
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             join b in context.Brands
                             on p.BrandId equals b.BrandId
                             join col in context.Colors
                             on p.ColorId equals col.ColorId

                             select new FavoriteDto
                             {
                                ProductId = p.ProductId,
                                AddDate = p.AddDate,
                                BarandId = p.BrandId,
                                BrandName = b.BrandName,
                                CategoryId = p.CategoryId,
                                CategoryName = c.CategoryName,
                                ColorId = p.ColorId,
                                ColorName = col.ColorName,
                                Description = p.Description,
                                FavoriteStatus = f.Status,
                                Id = f.Id,
                                ProductStatus = p.Status,
                                RentPrice = p.RentPrice,
                                SalePrice = p.SalePrice,
                                Title = p.Title,
                                ImagePaths = context.ProductImages.Where(p => p.ProductId == p.Id).ToList(),
                             };
                return result.ToList();

            }
        }
    }
}
