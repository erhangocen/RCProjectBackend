using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class BidImage : IEntity
    {
        public int Id { get; set; }
        public int BidId { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
    }
}
