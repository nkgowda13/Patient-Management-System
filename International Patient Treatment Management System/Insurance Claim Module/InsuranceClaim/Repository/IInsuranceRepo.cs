using InsuranceClaim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceClaim.Controllers;

namespace InsuranceClaim.Repository
{
    public interface IInsuranceRepo
    {
        public Task<List<InsurerDetail>> GetAllInsurerDetail();
        public Task<List<InsurerDetail>> GetInsurerByPackageName(string packageName);
        public List<string> GetBalance(InitiateClaim claim);

    }
}
