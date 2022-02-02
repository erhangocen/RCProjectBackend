using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class SaleDto : IDto
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string ProductTitle { get; set; }
        public string UserName { get; set; }
        public int Price { get; set; }
    }
}
