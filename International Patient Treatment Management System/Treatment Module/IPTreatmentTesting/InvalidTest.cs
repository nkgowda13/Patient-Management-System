using IPTreatment.Controllers;
using IPTreatment.Models;
using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TreatmentMicroservice.Models;

namespace IPTreatmentTesting
{
    public class InvalidTests
    {
        PackageDetail package = new PackageDetail();
        List<PackageDetail> packageDetails = new List<PackageDetail>();
        Mock<ITreatmentRepository> mockRepo;
        TreatmentRepository _repo;
        PatientDetail _detail = new PatientDetail();
        SpecialistDetail specialistDetail = new SpecialistDetail();
        List<SpecialistDetail> specialists = new List<SpecialistDetail>();
        private static readonly ILog _log4net = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TreatmentContext>();
            //optionsBuilder.UseSqlServer("Data Source=PC;User ID=sa;Database=DB5;");
            optionsBuilder.UseInMemoryDatabase("MyDB");
            var context = new TreatmentContext(optionsBuilder.Options);
            mockRepo = new Mock<ITreatmentRepository>();
            _repo = new TreatmentRepository(context);
            packageDetails = new List<PackageDetail>()
           {
               new PackageDetail { PackageId = 1, Ailment = "Orthopaedics", TreatmentPackageName = "Package 1", TestDetails = "OPT1, OPT2", Cost = 2500, TreatmentDuration = 4 },
               new PackageDetail { PackageId = 2, Ailment = "Orthopaedics", TreatmentPackageName = "Package 2", TestDetails = "OPT3,OPT4", Cost = 3000, TreatmentDuration = 6 },
               new PackageDetail { PackageId = 3, Ailment ="Urology", TreatmentPackageName = "Package 1", TestDetails = "UPT1,UPT2", Cost = 4000, TreatmentDuration = 4 },
               new PackageDetail { PackageId = 4, Ailment = "Urology", TreatmentPackageName = "Package 2", TestDetails = "UPT3,UPT4", Cost = 5000, TreatmentDuration =6 }
           };
            specialists = new List<SpecialistDetail>()
           {

                new SpecialistDetail { Id = 1, Name = "Shriny", ExperienceInYears = 4, ContactNumber = 8514235879, AreaOfExpertise = "Orthopaedics" },
                new SpecialistDetail { Id = 2, Name = "Joey", ExperienceInYears = 13, ContactNumber = 9875214000, AreaOfExpertise = "Orthopaedics" },
                new SpecialistDetail { Id = 3, Name = "Chandler", ExperienceInYears = 2, ContactNumber = 9965231470, AreaOfExpertise = "Urology" },
                new SpecialistDetail { Id = 4, Name = "Rachel", ExperienceInYears = 15, ContactNumber = 6358290001, AreaOfExpertise = "Urology" }

            };
        }

      
        [TestCase]
        public async Task TestingFormulateTimeTable()
        {
            TreatmentPlanController obj = new TreatmentPlanController(_repo);
            var result = await obj.FormulateTreatmentTimetable(new PatientDetail
            {
                Id = 1,
                Name = "Aman",
                Age = "30",
                Ailment = "OPT",
                TreatmentPackageName = "Package 3",
                TreatmentCommencementDate = new DateTime(2020, 1, 1),
            });
            var x =await _repo.GetAllMembers();
            Assert.IsFalse(x.Where(y=>y.PatientId==1).Any());
        }

        [TestCase]
        public async Task TestingGeneratePlan()
        {
            var mresult = await _repo.GeneratePlan(new PatientDetail
            {
                Id = 4,
                Name = "Ajay",
                Age = "120",
                Ailment = "Urology",
                TreatmentPackageName = "Package 6",
                TreatmentCommencementDate = new DateTime(2020, 1, 1),
            });
            Assert.IsNull(mresult);
        }


        [TestCase]
        public async Task TestingUpdateCost()
        {
            string result = await _repo.UpdateCost(0, 100, "Axis Health Insrurance");
            Assert.IsTrue(result == "False");
        }

    }
}