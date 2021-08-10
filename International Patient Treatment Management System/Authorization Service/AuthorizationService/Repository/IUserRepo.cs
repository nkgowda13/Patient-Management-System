using AuthorizationService.Models;

namespace AuthorizationService.Repository
{
    public interface IUserRepo
    {
        User GetMember(User user);
    }
}
