using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Core.Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEvaluationDal : EfEntityRepositoryBase<Evaluation, RCProjectDBContext>, IEvaluationDal
    {
        public List<EvaluationDto> GetEvaluationDetails(Expression<Func<Evaluation, bool>> filter = null)
        {
            using (RCProjectDBContext context = new RCProjectDBContext())
            {
                var result = from e in filter == null ? context.Evaluations : context.Evaluations.Where(filter)
                             join b in context.Bids
                             on e.BidId equals b.BidId
                             join u in context.Users
                             on b.UserId equals u.UserId
                             select new EvaluationDto
                             {
                                 BidId = b.BidId,
                                 UserId = u.UserId,
                                 BidDescription = b.Description,
                                 BidTitle = b.Title,
                                 BidValue = b.BidValue,
                                 EvaluationComment = e.Comment,
                                 EvaluationId = e.Id,
                                 EvaluationValue = e.EvaluationValue,
                                 UserName = u.Username,
                                 EvaluationDate = e.EvaluationDate
                             };
                return result.ToList();

            }
        }
        public List<EvaluationDto> GetEvaluationDetailsByUserId(Expression<Func<User, bool>> filter = null)
        {
            using (RCProjectDBContext context = new RCProjectDBContext())
            {
                var result = from e in context.Evaluations
                             join b in context.Bids
                             on e.BidId equals b.BidId
                             join u in filter == null ? context.Users : context.Users.Where(filter)
                             on b.UserId equals u.UserId
                             select new EvaluationDto
                             {
                                 BidId = b.BidId,
                                 UserId = u.UserId,
                                 BidDescription = b.Description,
                                 BidTitle = b.Title,
                                 BidValue = b.BidValue,
                                 EvaluationComment = e.Comment,
                                 EvaluationId = e.Id,
                                 EvaluationValue = e.EvaluationValue,
                                 UserName = u.Username,
                                 EvaluationDate = e.EvaluationDate
                             };
                return result.ToList();

            }
        }

    }
}
