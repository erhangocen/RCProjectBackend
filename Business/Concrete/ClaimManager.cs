using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
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

        public ClaimManager(IClaimDal claimDal, ITokenService tokenService)
        {
            _claimDal = claimDal;
        }

        [CacheRemoveAspect("IClaimService.GetAll")]
        public IResult Add(UserOperationClaim claim)
        {
            _claimDal.Add(claim);
            return new SuccessResult(Messages.AddClaim);
        }

        [CacheRemoveAspect("IClaimService.GetAll")]
        public IResult Delete(UserOperationClaim claim)
        {
            _claimDal.Delete(claim);
            return new SuccessResult(Messages.DeleteClaim);
        }

        [CacheRemoveAspect("IClaimService.GetAll")]
        public IResult DeleteUser(User user)
        {
            List<UserOperationClaim> claims = _claimDal.GetAll(c => c.UserId == user.UserId).ToList();
            foreach (var claim in claims)
            {
                _claimDal.Delete(claim);
            }
            return new SuccessResult(Messages.DeleteClaim);
        }

        [CacheAspect]
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

        [CacheRemoveAspect("IClaimService.GetAll")]
        public IResult Update(UserOperationClaim claim)
        {
            _claimDal.Update(claim);
            return new SuccessResult(Messages.updateClaim);
        }
    }
}
