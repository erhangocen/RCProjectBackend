using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class EvaluationManager : IEvaluationService
    {

        private IEvaluationDal _evaluationDal;

        public EvaluationManager(IEvaluationDal evaluationDal)
        {
            _evaluationDal = evaluationDal;
        }

        public IResult Add(Evaluation evaluation)
        {
            _evaluationDal.Add(evaluation);
            return new SuccessResult(Messages.EvaluationAdd);
        }

        public IResult Delete(Evaluation evaluation)
        {
            _evaluationDal.Delete(evaluation);
            return new SuccessResult(Messages.EvaluationDelete);
        }

        public IDataResult<List<Evaluation>> GetAll()
        {
            return new SuccessDataResult<List<Evaluation>>(_evaluationDal.GetAll());
        }

        public IDataResult<List<EvaluationDto>> GetAllDetails()
        {
            return new SuccessDataResult<List<EvaluationDto>>(_evaluationDal.GetEvaluationDetails());
        }

        public IDataResult<Evaluation> GetById(int id)
        {
            return new SuccessDataResult<Evaluation>(_evaluationDal.Get(p => p.Id == id));
        }

        public IDataResult<List<EvaluationDto>> GetByUserId(int id)
        {
            return new SuccessDataResult<List<EvaluationDto>>(_evaluationDal.GetEvaluationDetailsByUserId(p => p.UserId == id));
        }

        public IResult Update(Evaluation evaluation)
        {
            _evaluationDal.Update(evaluation);
            return new SuccessResult(Messages.EvaluationUpdate);
        }
    }
}
