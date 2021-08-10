using NUnit.Framework;
using Moq;
using InsuranceClaim.Controllers;
using InsuranceClaim.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using InsuranceClaim.Repository;
using System.Threading.Tasks;
//using System;
using InsuranceClaim.Data;
//using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceClaimTesting
{
    public class Tests : ControllerBase
    {
       private readonly InsurerDetail insurer = new();
        private readonly Task<List<InsurerDetail>> insurerDetails;
        Mock<IInsuranceRepo> mockInsurance;
        InsuranceRepo insuranceRepo;
        [SetUp]
        public void Setup()
        {
            var option = new DbContextOptionsBuilder<InsuranceClaimDbContext>().UseInMemoryDatabase(databaseName: "InsuranceClaim").Options;
            var context = new InsuranceClaimDbContext(option);
            mockInsurance = new Mock<IInsuranceRepo>();
            insuranceRepo = new InsuranceRepo(context);
            _ = new List<InsurerDetail>()
            {
                new InsurerDetail() { InsurerName = "LIC", InsurerPackageName = "GOLD", InsuranceAmountLimit = 5000, DisbursementDuration = "2 Years" },
                new InsurerDetail() { InsurerName = "HDFC", InsurerPackageName = "SILVER", InsuranceAmountLimit = 10000, DisbursementDuration = "5 Years" },
                new InsurerDetail() { InsurerName = "Bajaj", InsurerPackageName = "PLATINUM", InsuranceAmountLimit = 15000, DisbursementDuration = "10 Years" },
                new InsurerDetail() { InsurerName = "SBI", InsurerPackageName = "PREMIUM", InsuranceAmountLimit = 50000, DisbursementDuration = "15 Years" }
            };
        }

        [Test]
        public void GetAllInsurersDetails()
        {
            mockInsurance.Setup(x => x.GetAllInsurerDetail()).Returns(insurerDetails);
            var result = insuranceRepo.GetAllInsurerDetail();
            Assert.NotNull(result);

        }
        [TestCase("GOLD")]
        [TestCase("PLATINUM")]
        public void GetInsurerByPackageName(string packageName)
        {
            mockInsurance.Setup(x => x.GetInsurerByPackageName(packageName)).Returns(insurerDetails);
            var result = insuranceRepo.GetInsurerByPackageName(packageName);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAllInsurerDetails_ForController()
        {
            mockInsurance.Setup(x => x.GetAllInsurerDetail()).Returns(insurerDetails);
            InsuranceController pc = new(mockInsurance.Object);
            var result = pc.AllInsurerDetail();
            Assert.IsNotNull(result);

        }
        [TestCase("GOLD")]
        [TestCase("PLATINUM")]
        [TestCase("Average")]
        public void GetInsurerByPackageName_ForController(string packageName)
        {
                mockInsurance.Setup(x => x.GetInsurerByPackageName(packageName)).Returns(insurerDetails);
                InsuranceController pc = new(mockInsurance.Object);
                var result = pc.InsurerByPackageName(packageName);
                Assert.IsNotNull(result);
        }

    }
}