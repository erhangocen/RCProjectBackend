using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ProductDto : IDto
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public int BarandId { get; set; }
        public string BrandName { get; set; }
        public int RentPrice { get; set; }
        public int SalePrice { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AddDate { get; set; }
        public int Status { get; set; }
        public List<ProductImage> ImagePaths { get; set; }
    }
}
