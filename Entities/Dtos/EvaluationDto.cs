using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class EvaluationDto : IDto
    {
        public int EvaluationId { get; set; }
        public int BidId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string BidValue { get; set; }
        public int EvaluationValue { get; set; }
        public string BidTitle { get; set; }
        public string BidDescription { get; set; }
        public string EvaluationComment { get; set; }
        public DateTime EvaluationDate { get; set; }
    }
}
