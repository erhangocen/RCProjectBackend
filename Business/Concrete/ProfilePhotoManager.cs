using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProfilePhotoManager : IProfilePhotoService
    {
        IProfilePhotoDal _profilePhotoDal;
        public ProfilePhotoManager(IProfilePhotoDal profilePhotoDal)
        {
            _profilePhotoDal = profilePhotoDal;
        }

        public IResult Delete(ProfilePhoto profilePhoto)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<ProfilePhoto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<ProfilePhoto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<ProfilePhoto> GetByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(IFormFile formFile, ProfilePhoto profilePhoto)
        {
            throw new NotImplementedException();
        }
    }
}
