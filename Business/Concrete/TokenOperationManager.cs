using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class TokenOperationManager : ITokenOperationService
    {
        private ITokenOperationDal _tokenOperationDal;

        public TokenOperationManager(ITokenOperationDal tokenOperationDal)
        {
            _tokenOperationDal = tokenOperationDal;
        }

        [CacheRemoveAspect("ITokenOperationService.GetByUserId")]
        public IResult Add(TokenOperation tokenOperation)
        {
            _tokenOperationDal.Add(tokenOperation);
            return new SuccessResult(Messages.TokenOperationAdd);
        }

        public IDataResult<List<TokenOperation>> GetAll()
        {
            return new SuccessDataResult<List<TokenOperation>>(_tokenOperationDal.GetAll());
        }

        public IDataResult<TokenOperation> GetById(int id)
        {
            return new SuccessDataResult<TokenOperation>(_tokenOperationDal.Get(p => p.Id == id));
        }

        [CacheAspect]
        public IDataResult<List<TokenOperation>> GetByUserId(int userid)
        {
            return new SuccessDataResult<List<TokenOperation>>(_tokenOperationDal.GetAll(p => p.UserId == userid));
        }
    }
}
