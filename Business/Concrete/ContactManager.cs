using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ContactManager : IContactService
    {
        IContactDal _contactDal;
        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        [CacheRemoveAspect("IContactService.GetAll")]
        [ValidationAspect(typeof(ContactValidtor))]
        public IResult Add(Contact contact)
        {
            _contactDal.Add(contact);
            return new SuccessResult(Messages.SendMessage);
        }

        [CacheRemoveAspect("IContactService.GetAll")]
        public IResult Delete(Contact contact)
        {
            _contactDal.Delete(contact);
            return new SuccessResult(Messages.DeleteMessage);
        }

        [CacheAspect]
        public IDataResult<List<Contact>> GetAll()
        {
            return new SuccessDataResult<List<Contact>>(_contactDal.GetAll());
        }

        public IDataResult<List<ContactDto>> GetContactsDetails()
        {
            return new SuccessDataResult<List<ContactDto>>(_contactDal.GetContactDetails());
        }
    }
}
