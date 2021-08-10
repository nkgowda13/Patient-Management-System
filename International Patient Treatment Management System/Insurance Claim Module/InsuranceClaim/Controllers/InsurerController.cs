using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceClaim.Repository;
using InsuranceClaim.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
namespace InsuranceClaim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class InsuranceController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(InsuranceController));
        readonly IInsuranceRepo _repo;

        public InsuranceController(IInsuranceRepo repo)
        {
            _repo = repo;
        }


        //GET : api/Insurance/AllInsurerDetail    
        [HttpGet]
        [Route("AllInsurerDetail")]
        public async Task<ActionResult<InsurerDetail>> AllInsurerDetail()
        {
            _log4net.Info("Get Treatment Packages Service called..");

            var insurerDetails = await _repo.GetAllInsurerDetail();

            if (insurerDetails.Count == 0)
            {
                return BadRequest("No Details Found");
            }

            return Ok(insurerDetails);
        }

        [HttpGet]
        [Route("InsurerByPackageName")]
        public async Task<ActionResult<InsurerDetail>> InsurerByPackageName(string packageName)
        {
            var insurerDetail = await _repo.GetInsurerByPackageName(packageName);
            if (insurerDetail == null)
            {
                return BadRequest("Package Name Not Found");
            }
            return Ok(insurerDetail);
        }

        [HttpPost]
        [Route("InitiateClaim")]
        public IActionResult GetBalance(InitiateClaim claim)
        {
            if (claim is null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            var a = _repo.GetBalance(claim);
            return new OkObjectResult(a);
        }
    }
}
