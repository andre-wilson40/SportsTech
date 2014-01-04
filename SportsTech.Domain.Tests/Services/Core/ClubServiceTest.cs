using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTech.Domain.Services.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsTech.Domain.Tests.Services.Core
{
    [TestClass]
    public class ClubServiceTest
    {
        [TestMethod]
        public async Task HasNoClubs()
        {
            // Arrange
            var dataContext = MockFactory.GetEmptyContext();
            dataContext.Clubs = MockFactory.CreateDbSet<Data.Model.Club>(new List<Data.Model.Club>()).Object;
            var uow = MockFactory.GetUnitOfWork(dataContext);

            var userService = MockFactory.GetUserService(uow);

            var eventService = new ClubService(uow, userService.Object);

            // Act
            var records = await eventService.GetAllAsync();

            // assert
            Assert.IsTrue(records.Count == 0);
        }

        [TestMethod]
        public async Task HasOneClub()
        {
            // Arrange
            var dataContext = MockFactory.GetEmptyContext();
            dataContext.Clubs = MockFactory
                .CreateDbSet<Data.Model.Club>(new List<Data.Model.Club>
                {
                    new Data.Model.Club { 
                        Id = 1, 
                        Name = "Waihou",
                        UserProfiles = new List<Data.Model.UserProfile>
                        {
                            MockFactory.GetUserProfile()
                        }
                    },                    
                })
                .Object;
            
            var uow = MockFactory.GetUnitOfWork(dataContext);

            var userService = MockFactory.GetUserService(uow);

            var eventService = new ClubService(uow, userService.Object);

            // Act
            var records = await eventService.GetAllAsync();

            // assert
            Assert.IsTrue(records.Count == 1);
        }

        [TestMethod]
        public async Task HasManyClubs()
        {
            // Arrange
            var dataContext = MockFactory.GetEmptyContext();
            dataContext.Clubs = MockFactory
                .CreateDbSet<Data.Model.Club>(new List<Data.Model.Club>
                {
                    new Data.Model.Club { Id = 1, Name = "Waihou", UserProfiles = new List<Data.Model.UserProfile>
                        {
                            MockFactory.GetUserProfile()
                        }
                    },
                    new Data.Model.Club { Id = 2, Name = "Morrinsville",
                        UserProfiles = new List<Data.Model.UserProfile>
                        {
                            MockFactory.GetUserProfile()
                        }
                    }
                })
                .Object;

            var uow = MockFactory.GetUnitOfWork(dataContext);

            var userService = MockFactory.GetUserService(uow);

            var eventService = new ClubService(uow, userService.Object);

            // Act
            var records = await eventService.GetAllAsync();

            // assert
            Assert.IsTrue(records.Count == 2);
        }
    }
}
