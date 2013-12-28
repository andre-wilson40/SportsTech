using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTech.Domain.Services.Core;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace SportsTech.Domain.Tests.Services.Core
{
    [TestClass]
    public class EventServiceTest
    {
        [TestMethod]
        public async Task GetAllNoRecords()
        {
            // Arrange
            var dataContext = MockFactory.GetEmptyContext();
            var eventService = new EventService(dataContext);

            // Act
            var records = await eventService.GetAllAsync();

            // Assert
            Assert.IsTrue(!records.Any());
        }

        [TestMethod]
        public async Task GetAllOneRecord()
        {
            // Arrange
            var data = new List<Data.Model.Event>() {
                new Data.Model.Event()
            };

            var dataContext = MockFactory.GetEmptyContext();
            dataContext.Events = MockFactory.CreateDbSet<Data.Model.Event>(data).Object;

            var eventService = new EventService(dataContext);

            // Act
            var records = await eventService.GetAllAsync();

            // Assert
            Assert.AreEqual(1, records.Count);
        }

        [TestMethod]
        public async Task GetById()
        {
            // Arrange
            var data = new List<Data.Model.Event>() {
                new Data.Model.Event { Id = 2 }
            };

            var dataContext = MockFactory.GetEmptyContext();
            dataContext.Events = MockFactory.CreateDbSet<Data.Model.Event>(data).Object;

            var eventService = new EventService(dataContext);

            // Act
            var record = await eventService.SingleAsync(2);

            // Assert
            Assert.IsNotNull(record);
            Assert.AreEqual(2, record.Id);
        }
    }
}
