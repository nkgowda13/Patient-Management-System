using InsuranceClaim.Data;
using InsuranceClaim.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceClaim.Repository
{
    public class InsuranceRepo : IInsuranceRepo
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(InsuranceRepo));
        private readonly InsuranceClaimDbContext _context;
        public InsuranceRepo(InsuranceClaimDbContext context)
        {
            _context = context;
            if (_context.InsurerDetails.Any())
            {
                return;
            }

            _context.InsurerDetails.AddRange(
                new InsurerDetail() { InsurerName = "LIC", InsurerPackageName = "GOLD", InsuranceAmountLimit = 5000, DisbursementDuration = "2 Years" },
                new InsurerDetail() { InsurerName = "HDFC", InsurerPackageName = "SILVER", InsuranceAmountLimit = 10000, DisbursementDuration = "5 Years" },
                new InsurerDetail() { InsurerName = "Bajaj", InsurerPackageName = "PLATINUM", InsuranceAmountLimit = 15000, DisbursementDuration = "10 Years" },
                new InsurerDetail() { InsurerName = "SBI", InsurerPackageName = "PREMIUM", InsuranceAmountLimit = 50000, DisbursementDuration = "15 Years" }
            );

            _context.InitiateClaims.AddRange(
                new InitiateClaim() { PatientName = "Rio", Ailment = "Orthopaedics", TreatmentPackageName = "Package 1", InsurerName = "LIC", TotalInsuranceCost = 10000 },
                new InitiateClaim() { PatientName = "Tokyo", Ailment = "Urology", TreatmentPackageName = "Package 2", InsurerName = "SBI", TotalInsuranceCost = 35000 },
                new InitiateClaim() { PatientName = "Berlin", Ailment = "Urology", TreatmentPackageName = "Package 1", InsurerName = "Bajaj", TotalInsuranceCost = 17000 },
                new InitiateClaim() { PatientName = "Nerobi", Ailment = "Orthopaedics", TreatmentPackageName = "Package 2", InsurerName = "HDFC", TotalInsuranceCost = 9000 }
            );
            _context.SaveChanges();
        }

        public async Task<List<InsurerDetail>> GetAllInsurerDetail()
        {
            List<InsurerDetail> details = new();
            try
            {
                details = await _context.InsurerDetails.ToListAsync();
                if (details == null)
                    return null;
            }
            catch (Exception exception)
            {
                _log4net.Error(exception.Message);

                return null;
            }
            _log4net.Info("Package details is fetched  Successfully");
            return details;
        }

        public async Task<List<InsurerDetail>> GetInsurerByPackageName(string packageName)
        {
            List<InsurerDetail> detailsByNames = new();
            try
            {
                detailsByNames = await _context.InsurerDetails.Where
                    (detail => detail.InsurerPackageName == packageName).ToListAsync();
                if (detailsByNames == null)
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                _log4net.Error(exception.Message);

            }
            _log4net.Info("Package Name is fetched  Successfully");
            return detailsByNames;
        }

        public List<string> GetBalance(InitiateClaim claim)
        {
            var Insurer = _context.InsurerDetails.Where(p => p.InsurerName == claim.InsurerName).FirstOrDefault();
            _context.InitiateClaims.Add(claim);
            _context.SaveChanges();
            if (claim.TotalInsuranceCost <= Insurer.InsuranceAmountLimit)
                return null;
            string balance = Convert.ToString(claim.TotalInsuranceCost - Insurer.InsuranceAmountLimit);
            string name = Insurer.InsurerName;
            return (new List<string> {
                balance,
                name
            });
        }


    }
}
