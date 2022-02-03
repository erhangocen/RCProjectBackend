using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class UserManager:IUserService
    {  
        private readonly IUserDal _userDal;
        private readonly IClaimService _claimService;
        private readonly ITokenService _tokenService;

        public UserManager(IUserDal userDal,
            IClaimService claimService,
            ITokenService tokenService)
        {
            _userDal = userDal;
            _claimService = claimService;
            _tokenService = tokenService;
        }

        [CacheRemoveAspect("IUserService.GetClaims")]
        [CacheRemoveAspect("IUserService.GetSingleUserDetail")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            var checkMail = CheckOtherEmails(user.Email);
            var checkUsername = CheckOtherUsernames(user.Username);

            var result = BusinessRules.Run(checkMail,checkUsername);

            if (result != null)
            {
                return result;
            }

            _userDal.Add(user);
            return new SuccessResult();
        }
        
        [CacheRemoveAspect("IUserService.GetClaims")]
        [CacheRemoveAspect("IUserService.GetSingleUserDetail")]
        [ValidationAspect(typeof(UserValidator))]
        [TransactionScopeAspect]
        public IResult AddEditor(User user)
        {
            Add(user);

            UserOperationClaim userOperationClaim = new UserOperationClaim()
            {
                OperationClaimId = 2,
                UserId = user.UserId
            };

            _claimService.Add(userOperationClaim);
            return new SuccessResult(Messages.SuccessCreateEditor);
        }

        [CacheRemoveAspect("IUserService.GetClaims")]
        [CacheRemoveAspect("IUserService.GetSingleUserDetail")]
        [TransactionScopeAspect]
        public IResult Delete(User user)
        {
            _claimService.DeleteUser(user);
            _userDal.Delete(user);
            return new SuccessResult(Messages.SuccessDeleteAccount);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }


        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        [CacheAspect]
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IDataResult<List<UserDto>> GetUserDetails()
        {
            return new SuccessDataResult<List<UserDto>>(_userDal.GetUserDetails());
        }

        [CacheAspect]
        public IDataResult<UserDto> GetSingleUserDetail(int userId)
        {
            return new SuccessDataResult<UserDto>(_userDal.GetSingleUserDetails(userId));
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.GetClaims")]
        [CacheRemoveAspect("IUserService.GetSingleUserDetail")]
        public IResult Update(User user)
        {
            var checkMail = CheckOtherEmails(user.Email);
            var checkUsername = CheckOtherUsernames(user.Username);

            var result = BusinessRules.Run(checkMail, checkUsername);

            if (result != null)
            {
                return result;
            }

            _userDal.Update(user);
            return new SuccessResult(Messages.SuccessUpdateAccount);
        }

        private IResult CheckOtherEmails(string email)
        {
            var result = _userDal.GetAll(u => u.Email == email).Any();
            if (result)
            {
                return new ErrorResult(Messages.EmailInvalid2);
            }

            return new SuccessResult();
        }

        private IResult CheckOtherUsernames(string username)
        {
            var result = _userDal.GetAll(u => u.Username == username).Any();
            if (result)
            {
                return new ErrorResult(Messages.EmailInvalid3);
            }

            return new SuccessResult();
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.UserId == id));
        }

        [CacheRemoveAspect("IUserService.GetClaims")]
        [CacheRemoveAspect("IUserService.GetSingleUserDetail")]
        [TransactionScopeAspect]
        public IResult AddCustomer(User user)
        {
            Add(user);
            UserOperationClaim userOperationClaim = new UserOperationClaim()
            {
                OperationClaimId = 3,
                UserId = user.UserId
            };
            Token token = new Token()
            {
                TokenValue = 0,
                UserId = user.UserId
            };
            _claimService.Add(userOperationClaim);
            _tokenService.Add(token);
            return new SuccessResult(Messages.SuccessCreateAccount);
        }

        public IDataResult<User> GetByUsername(string username)
        {
            return new SuccessDataResult<User>(_userDal.Get(p => p.Username == username));
        }
    }
}
