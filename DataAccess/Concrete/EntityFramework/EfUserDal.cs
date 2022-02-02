using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal:EfEntityRepositoryBase<User,RCProjectDBContext>,IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new RCProjectDBContext())
            {
                var result = from operationClaim in context.OperationClaims
                    join userOperationClaim in context.UserOperationClaims
                        on operationClaim.Id equals userOperationClaim.OperationClaimId
                    where userOperationClaim.UserId == user.UserId
                    select new OperationClaim {Id = operationClaim.Id, Name = operationClaim.Name};
                return result.ToList();
            }
        }

        public UserDto GetSingleUserDetails(int userId)
        {
            using (RCProjectDBContext context = new RCProjectDBContext())
            {
                var result = from u in context.Users.Where(u=>u.UserId == userId)
                             select new UserDto
                             {
                                 UserId = u.UserId,
                                 Claims = this.GetClaims(u),
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email
                             };
                return result.FirstOrDefault();
            }
        }

        public List<UserDto> GetUserDetails()
        {
            using (RCProjectDBContext context = new RCProjectDBContext())
            {
                var result = from u in context.Users
                             select new UserDto
                             {
                                 UserId = u.UserId,
                                 Claims = this.GetClaims(u),
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email
                             };
                return result.ToList();
            }
        }


    }
}
