using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccsessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.WrongPassword);
            }
            return new SuccessDataResult<User>(userToCheck, Messages.SuccesLogin);
        }

        //[ValidationAspect(typeof(RegisterValidator))]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Username = userForRegisterDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userService.AddCustomer(user);
            return new SuccessDataResult<User>(user, Messages.SuccesRegister);
        }

        //[ValidationAspect(typeof(RegisterValidator))]
        public IDataResult<User> Update(UserForUpdateDto userForUpdateDto)
        {
            User user = _userService.GetById(userForUpdateDto.UserId).Data;
            user.FirstName = userForUpdateDto.FirstName;
            user.LastName = userForUpdateDto.LastName;

            _userService.Update(user);
            return new SuccessDataResult<User>(user, Messages.SuccesUpdateUser);
        }

        public IResult UserExist(string email, string username)
        {
            if (_userService.GetByMail(email).Data != null || _userService.GetByUsername(username).Data != null)
            {
                return new ErrorResult(Messages.UserAlredyExist);
            }
            return new SuccessResult();
        }
    }
}