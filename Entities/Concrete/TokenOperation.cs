using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class TokenOperation : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationValue { get; set; }
        public DateTime OperationDate { get; set; }
        public int Status { get; set; }
    }
}
