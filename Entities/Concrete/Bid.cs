using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Bid : IEntity
    {
        public int BidId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BidValue { get; set; }
        public DateTime BidDate { get; set; }
        public string Status { get; set; }
    }
}
