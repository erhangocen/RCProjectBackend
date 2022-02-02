using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace Entities.Dtos
{
    public class RentalDto : IDto
    {
        public int RentalId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string ProductTitle { get; set; }
        public string UserName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int TotalPrice { get; set; }
    }
}
