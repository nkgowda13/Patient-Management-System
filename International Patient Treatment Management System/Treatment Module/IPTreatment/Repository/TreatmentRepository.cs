using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TreatmentMicroservice.Models;

namespace IPTreatment.Models
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private TreatmentContext _context;
        private HttpClient _client;
        private HttpResponseMessage _response;
        private IConfiguration _config;

        public TreatmentRepository(TreatmentContext _context,IConfiguration config)
        {
            this._context = _context;
            this._config = config;
            this._client = new HttpClient();
        }
        public TreatmentRepository(TreatmentContext _context)
        {
            this._context = _context;
        }
        public async Task<TreatmentPlan> GeneratePlan(PatientDetail _detail)
        {
            int patientCount = (from x in _context.Patients select x).Count();
            _detail.Id = ++patientCount;

            //Receiving package and specialist lists                
            List<PackageDetail> packageList = await GetPackageList();
            List<SpecialistDetail> specialistList = await GetSpecialists();

            TreatmentPlan plan = null;

            if (packageList != null && specialistList != null)
            {
                PackageDetail package = (from x in packageList
                                         where x.TreatmentPackageName == _detail.TreatmentPackageName
                                         && x.Ailment == _detail.Ailment select x).SingleOrDefault<PackageDetail>();
                SpecialistDetail specialist;
                // Package 2 coresponds to senior doctor
                if (_detail.TreatmentPackageName == "Package 2")
                {
                    specialist = (from x in specialistList
                                  where x.AreaOfExpertise == _detail.Ailment
                                  && x.ExperienceInYears >= 8
                                  select x).SingleOrDefault<SpecialistDetail>();
                }
                else
                {
                    specialist = (from x in specialistList
                                  where x.AreaOfExpertise == _detail.Ailment
                                  && x.ExperienceInYears < 8
                                  select x).SingleOrDefault<SpecialistDetail>();
                } 
                if (specialist==null || package==null) return null;
                int plansCount = (from x in _context.Plans select x).Count();
                plan = new TreatmentPlan()
                {
                    PlanId = ++plansCount,
                    PatientId = _detail.Id,
                    PatientName = _detail.Name,
                    PackageName = package.TreatmentPackageName,
                    TestDetails = package.TestDetails,
                    Cost = package.Cost,
                    SpecialistName = specialist.Name,
                    TreatmentCommencementDate = _detail.TreatmentCommencementDate,
                    TreatmentEndDate = _detail.TreatmentCommencementDate.AddDays(package.TreatmentDuration * 7),
                    Status = "In-Progress",
                    Insurer = "Insurer to be assigned"
                };
            }
            if (!await SaveAll(_detail, plan))
            {
                plan = null;
            }
            return plan;
        }

        public async Task<List<PackageDetail>> GetPackageList()
        {
            //_client = new HttpClient();
            //string uriConn = _config.GetValue<string>("Link:offeringUri");
            //_client.BaseAddress = new Uri(uriConn + "api/offering/IpTreatmentPackages");
            //log.Info("Pateinsts Package List from Offering Microservice is being invoked");
            //List<PackageDetail> packageList;
            //_response = new HttpResponseMessage();
            //_response = _client.GetAsync(_client.BaseAddress).Result;
            //string apiResponse = await _response.Content.ReadAsStringAsync();
            //packageList = JsonConvert.DeserializeObject<List<PackageDetail>>(apiResponse);
            //return packageList;
            var x = new List<PackageDetail>()
           {
               new PackageDetail { PackageId = 1, Ailment = "Orthopaedics", TreatmentPackageName = "Package 1", TestDetails = "OPT1, OPT2", Cost = 10000, TreatmentDuration = 4 },
               new PackageDetail { PackageId = 2, Ailment = "Orthopaedics", TreatmentPackageName = "Package 2", TestDetails = "OPT3,OPT4", Cost = 9000, TreatmentDuration = 6 },
               new PackageDetail { PackageId = 3, Ailment ="Urology", TreatmentPackageName = "Package 1", TestDetails = "UPT1,UPT2", Cost = 17000, TreatmentDuration = 4 },
               new PackageDetail { PackageId = 4, Ailment = "Urology", TreatmentPackageName = "Package 2", TestDetails = "UPT3,UPT4", Cost = 35000, TreatmentDuration =6 }
           };
            return x;
        }

        public async Task<List<SpecialistDetail>> GetSpecialists()
        {
            log.Info("Pateinsts Specialist List from Offering Microservice is being invoked");
            //_client = new HttpClient();
            //string uriConn = _config.GetValue<string>("Link:offeringUri");
            //_client.BaseAddress = new Uri(uriConn + "api/offering/Specialists");
            //List<SpecialistDetail> specialists;
            //_response = new HttpResponseMessage();
            //_response = _client.GetAsync(_client.BaseAddress).Result;
            //string apiResponse = await _response.Content.ReadAsStringAsync();
            //specialists = JsonConvert.DeserializeObject<List<SpecialistDetail>>(apiResponse);
            //return specialists;
            var x = new List<SpecialistDetail>()
           {

                new SpecialistDetail { Id = 1, Name = "Shriny", ExperienceInYears = 4, ContactNumber = 8514235879, AreaOfExpertise = "Orthopaedics" },
                new SpecialistDetail { Id = 2, Name = "Joey", ExperienceInYears = 13, ContactNumber = 9875214000, AreaOfExpertise = "Orthopaedics" },
                new SpecialistDetail { Id = 3, Name = "Chandler", ExperienceInYears = 2, ContactNumber = 9965231470, AreaOfExpertise = "Urology" },
                new SpecialistDetail { Id = 4, Name = "Rachel", ExperienceInYears = 15, ContactNumber = 6358290001, AreaOfExpertise = "Urology" }

            };
            return x;
        }

        public async Task<bool> SaveAll(PatientDetail _detail, TreatmentPlan _plan)
        {
            log.Info("Patient details and plan detail are being saved");
            try
            {
                _context.Patients.Add(_detail);
                _context.Plans.Add(_plan);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("There is no patient details available" + ex.Message);
                return false;
            }
        }

        public async Task<string> UpdateCost(int id, int cost, string insurer)
        {
            try
            {
                TreatmentPlan plan = _context.Plans.Where(i => i.PlanId == id).FirstOrDefault();
                plan.Cost = cost;
                plan.Status = "Completed";
                plan.Insurer = insurer;
                _context.Plans.Update(plan);
                await _context.SaveChangesAsync();
                return plan.Status;
            }
            catch
            {
                log.Error("Failed to cost cost for patient");
                return "False";
            }
        }

        public async Task<List<TreatmentPlan>> GetPaymentInProgress()
        {
            log.Info("Payment is in progres...");
            try
            {
                List<TreatmentPlan> details = _context.Plans.Where(i => i.Status != "Completed").ToList();
                return details;
            }
            catch
            {
                log.Error("Payment Failed");
                return null;
            }
        }

        public async Task<List<TreatmentPlan>> GetAllMembers()
        {
            log.Info("Getting member details...");
            try
            {
                List<TreatmentPlan> details = _context.Plans.ToList();
                return details;
            }
            catch
            {
                log.Error("Failed to fetch member deatils.");
                return null;
            }
        }
    }
}
