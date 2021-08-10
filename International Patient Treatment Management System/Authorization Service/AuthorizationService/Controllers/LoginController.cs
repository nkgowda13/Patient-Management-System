using AuthorizationService.Models;
using AuthorizationService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace AuthorizationService.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ITokenRepo _repository;
        private readonly IUserRepo _memberRepo;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));
        public LoginController(ITokenRepo repository, IUserRepo memberRepo, IConfiguration config)
        {
            _repository = repository;
            _config = config;
            _memberRepo = memberRepo;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] User model)
        {
            try
            {
                _log4net.Info("Authorization has been initiated.");
                User member = _memberRepo.GetMember(model);
                if (member != null)
                {
                    _log4net.Info("User in database. Creating token.");
                    var tokenString = _repository.CreateJWT(_config, member);
                    _log4net.Info("Token created.");
                    _log4net.Info("Authorization granted.");
                    return Ok(new { token = tokenString });
                }
                _log4net.Info("User not in database. Authorization denied.");
                return Unauthorized("Invalid Credentials");
            }
            catch (Exception e)
            {
                _log4net.Info("Error Occured. Authorization failed.");
                return BadRequest(e.Message);
            }
        }
    }
}
