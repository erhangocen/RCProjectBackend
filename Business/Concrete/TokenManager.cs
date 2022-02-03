using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class TokenManager : ITokenService
    {

        private ITokenDal _tokenDal;

        public TokenManager(ITokenDal tokenDal)
        {
            _tokenDal = tokenDal;
        }

        public IResult Add(Token token)
        {
            _tokenDal.Add(token);
            return new SuccessResult(Messages.TokenAdd);
        }

        public IResult Delete(Token token)
        {
            _tokenDal.Delete(token);
            return new SuccessResult(Messages.TokenDelete);
        }

        public IDataResult<List<Token>> GetAll()
        {
            return new SuccessDataResult<List<Token>>(_tokenDal.GetAll());
        }

        public IDataResult<Token> GetById(int id)
        {
            return new SuccessDataResult<Token>(_tokenDal.Get(p => p.Id == id));
        }

        public IDataResult<Token> GetByUserId(int id)
        {
            return new SuccessDataResult<Token>(_tokenDal.Get(p => p.UserId == id));
        }

        public IResult Update(Token token)
        {
            _tokenDal.Update(token);
            return new SuccessResult(Messages.TokenUpdate);
        }
    }
}
