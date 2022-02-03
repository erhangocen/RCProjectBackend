using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfContactDal: EfEntityRepositoryBase<Contact, RCProjectDBContext>, IContactDal
    {
        public List<ContactDto> GetContactDetails()
        {
            using (RCProjectDBContext context = new RCProjectDBContext())
            {
                var result = from c in context.Contacts
                             join u in context.Users
                             on c.UserId equals u.UserId
                             select new ContactDto
                             {
                                 Id = c.Id,
                                 UserId = u.UserId,
                                 FullName = u.FirstName + " " + u.LastName,
                                 Username = u.Username,
                                 ContInfo = c.ContInfo,
                                 Message = c.Message,
                                 ImagePath = context.ProfilePhotos.Where(i => i.UserId == u.UserId).FirstOrDefault().ImagePath
                             };
                return result.ToList();
            }
        }
    }
}
