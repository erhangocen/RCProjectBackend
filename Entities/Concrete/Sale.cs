using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Sale:IEntity
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
