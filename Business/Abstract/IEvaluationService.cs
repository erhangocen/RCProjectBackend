using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IEvaluationService
    {
        IDataResult<List<Evaluation>> GetAll();
        IDataResult<Evaluation> GetById(int id);
        IDataResult<List<EvaluationDto>> GetByUserId(int id);
        IDataResult<List<EvaluationDto>> GetAllDetails();
        IResult Add(Evaluation evaluation);
        IResult Update(Evaluation evaluation); 
        IResult Delete(Evaluation evaluation);
    }
}
