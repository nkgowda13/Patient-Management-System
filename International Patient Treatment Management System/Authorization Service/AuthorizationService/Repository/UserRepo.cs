using AuthorizationService.Models;
using AuthorizationService.Provider;
using System;
using System.Linq;

namespace AuthorizationService.Repository
{
    public class UserRepo : IUserRepo
    {
        private UserDbContext _userDbContext;
        public UserRepo(UserDbContext userDbContext)
        {
            try
            {
                _userDbContext = userDbContext;
                if (_userDbContext.users.Any())
                {
                    return;
                }

                _userDbContext.AddRange(
                    new User { Username = "admin", Password = "admin" }
                    );

                _userDbContext.SaveChanges();
            }
            catch(Exception)
            {
                return;
            }
        }
        public User GetMember(User user)
        {
            try
            {
                var final = _userDbContext.users.Where(m => m.Username == user.Username && m.Password == user.Password).FirstOrDefault();
                return final;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
