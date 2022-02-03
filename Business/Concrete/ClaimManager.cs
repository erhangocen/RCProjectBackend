using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccsess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ClaimManager : IClaimService
    {
        private IClaimDal _claimDal;
        private ITokenService _tokenService;

        public ClaimManager(IClaimDal claimDal, ITokenService tokenService)
        {
            _claimDal = claimDal;
            _tokenService = tokenService;
        }

        public IResult Add(UserOperationClaim claim)
        {
            _claimDal.Add(claim);
            return new SuccessResult(Messages.AddClaim);
        }

        public IResult AddEditor(User user)
        {
            UserOperationClaim claim = new UserOperationClaim()
            {
                OperationClaimId = 2,
                UserId = user.UserId
            };
            _claimDal.Add(claim);
            return new SuccessResult(Messages.AddClaim);
        }

        public IResult AddUser(User user)
        {
            UserOperationClaim claim = new UserOperationClaim()
            {
                OperationClaimId = 3,
                UserId = user.UserId
            };

            Token token = new Token()
            {
                TokenValue = 0,
                UserId = user.UserId
            };

            _claimDal.Add(claim);
            _tokenService.Add(token);
            return new SuccessResult(Messages.AddClaim);
        }

        public IResult Delete(UserOperationClaim claim)
        {
            _claimDal.Delete(claim);
            return new SuccessResult(Messages.DeleteClaim);
        }

        public IResult DeleteUser(User user)
        {
            List<UserOperationClaim> claims = _claimDal.GetAll(c => c.UserId == user.UserId).ToList();
            foreach (var claim in claims)
            {
                _claimDal.Delete(claim);
            }
            return new SuccessResult(Messages.DeleteClaim);
        }

        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_claimDal.GetAll());
        }

        public IDataResult<List<UserOperationClaim>> GetByClaimId(int id)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_claimDal.GetAll(c => c.OperationClaimId == id));
        }

        public IDataResult<List<UserOperationClaim>> GetByUserId(int id)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_claimDal.GetAll(c => c.UserId == id));
        }

        public IResult Update(UserOperationClaim claim)
        {
            _claimDal.Update(claim);
            return new SuccessResult(Messages.updateClaim);
        }
    }
}
