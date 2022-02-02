using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IEvaluationDal : IEntityRepository<Evaluation>
    {
        List<EvaluationDto> GetEvaluationDetails(Expression<Func<Evaluation, bool>> filter = null);
        List<EvaluationDto> GetEvaluationDetailsByUserId(Expression<Func<User, bool>> filter = null);
    }
}
