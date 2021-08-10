using AuthorizationService.Controllers;
using AuthorizationService.Models;
using AuthorizationService.Provider;
using AuthorizationService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AuthTest
{
    public class Tests
    {
        IConfiguration _config;
        UserRepo _userRepo;
        TokenRepo _tokenRepo;
        LoginController _controller;
        User user1 = new User { Username = "admin", Password = "admin" };
        User user2 = new User { Username = "user", Password = "user" };

        [SetUp]
        public void SetUp()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key","123456789ABCDEFGH" },
                {"Jwt:Issuer", "https://localhost:44392" }
            };
            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var option = new DbContextOptionsBuilder<UserDbContext>().UseInMemoryDatabase(databaseName: "User").Options;
            var context = new UserDbContext(option);
            _userRepo = new UserRepo(context);
            _tokenRepo = new TokenRepo();
            _controller = new LoginController(_tokenRepo, _userRepo, _config);
        }

        [Test]
        public void GetMemberFromControllerPositive()
        {
            var response = _controller.Login(user1);
            var result = response as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
        }
        [Test]
        public void GetMemberFromControllerNegative()
        {
            var response = _controller.Login(user2);
            var result = response as ObjectResult;
            Assert.AreEqual(401, result.StatusCode);
        }
        [Test]
        public void GetMemberFromRepoPositive()
        {
            var response = _userRepo.GetMember(user1);
            Assert.IsNotNull(response);
        }
        [Test]
        public void GetMemberFromRepoNegative()
        {
            var response = _userRepo.GetMember(user2);
            Assert.IsNull(response);
        }
        [Test]
        public void GetTokenPositive()
        {
            var response = _tokenRepo.CreateJWT(_config, user1);
            Assert.IsNotNull(response);
        }
    }
}