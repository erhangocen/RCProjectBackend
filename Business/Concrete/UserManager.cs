using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
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
        private IUserDal _userDal;
        private IClaimService _claimService;


        public UserManager(IUserDal userDal,
            IClaimService claimService)
        {
            _userDal = userDal;
            _claimService = claimService;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            var check = CheckOtherEmail(user.Email);

            var result = BusinessRules.Run(check);

            if (result != null)
            {
                return result;
            }
            _userDal.Add(user);
            _claimService.AddUser(user);
            return new SuccessResult(Messages.SuccessCreateAccount);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult AddEditor(User user)
        {
            var check = CheckOtherEmail(user.Email);


            var result = BusinessRules.Run(check);

            if (result != null)
            {
                return result;
            }
            _userDal.Add(user);
            _claimService.AddEditor(user);
            return new SuccessResult(Messages.SuccessCreateAccount);
        }

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

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IDataResult<List<OperationClaim>> GetOperationClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IDataResult<List<UserDto>> GetUserDetails()
        {
            return new SuccessDataResult<List<UserDto>>(_userDal.GetUserDetails());
        }

        public IDataResult<UserDto> GetSingleUserDetail(int userId)
        {
            return new SuccessDataResult<UserDto>(_userDal.GetSingleUserDetails(userId));
        }

        //[ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.SuccessUpdateAccount);
        }

        private IResult CheckOtherEmail(string email)
        {
            var result = _userDal.GetAll(u => u.Email == email).Any();
            if (result)
            {
                return new ErrorResult(Messages.EmailInvalid2);
            }

            return new SuccessResult();
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.UserId == id));
        }
    }
}
