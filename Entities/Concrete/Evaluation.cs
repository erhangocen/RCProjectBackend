using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Evaluation: IEntity
    {
        public int Id { get; set; }
        public int BidId { get; set; }
        public int EvaluationValue { get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }
    }
}
