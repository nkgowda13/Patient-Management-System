using AuthorizationService.Models;
using Microsoft.Extensions.Configuration;

namespace AuthorizationService.Repository
{
    public interface ITokenRepo
    {
        public string CreateJWT(IConfiguration config, User user);
    }
}
