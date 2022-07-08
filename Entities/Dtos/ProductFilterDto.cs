using Core.Entities;
using Core.Utilities.Attributes.FilterAttributes;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ProductFilterDto : IFilterDto
    {
        [EqualFilter("ProductId")]
        public int ProductId { get; set; }

        [EqualFilter("CategoryId")]
        public int CategoryId { get; set; }

        [EqualFilter("CategoryName")]
        public string CategoryName { get; set; }

        [EqualFilter("ColorId")]
        public int ColorId { get; set; }

        [EqualFilter("ColorName")]
        public string ColorName { get; set; }

        [EqualFilter("BrandId")]
        public int BarandId { get; set; }

        [EqualFilter("BrandName")]
        public string BrandName { get; set; }

        [EqualFilter("RentPrice")]
        public int RentPrice { get; set; }

        [EqualFilter("SalePrice")]
        public int SalePrice { get; set; }

        [EqualFilter("Title")]
        public string Title { get; set; }

        [EqualFilter("Description")]
        public string Description { get; set; }

        [EqualFilter("AddDate")]
        public DateTime AddDate { get; set; }

        [EqualFilter("Status")]
        public int Status { get; set; }

        [EqualFilter("ImagePaths")]
        public List<ProductImage> ImagePaths { get; set; }
    }
}
