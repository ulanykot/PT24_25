using System;
using System.Collections.Generic;
using DataTask1;
using LogicTask1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestTask1
{
    [TestClass]
    public class HotelManagerTests
    {
        private IDataRepository _dataRepository = new DataRepository(new DataContext());
        private HotelManager _hotelManager = new HotelManager(new DataRepository(new DataContext()));

        [TestInitialize]
        public void Setup()
        {
            var context = new DataContext();

            _dataRepository = new DataRepository(context);
            _hotelManager = new HotelManager(_dataRepository);
        }

        [TestMethod]
        public void AddUser_ValidUser_UserAddedSuccessfully()
        {
            // Arrange
            User user = TestDataGenerator.GenerateUser(1, "John", "Doe");

            // Act
            _hotelManager.AddUser(user);

            // Assert
            var users = _dataRepository.GetAllUsers();
            Assert.IsTrue(users.Contains(user));
        }

        [TestMethod]
        public void AddRoomToCatalog_ValidRoom_RoomAddedSuccessfully()
        {
            // Arrange
            Catalog room = TestDataGenerator.GenerateCatalog(101, RoomType.Vip);

            // Act
            _hotelManager.AddRoomToCatalog(101, room);

            // Assert
            var rooms = _dataRepository.GetAllStates();
            Assert.IsTrue(rooms.Exists(r => r.RoomCatalog == room));
        }

        [TestMethod]
        public void MakeBooking_ValidBooking_BookingEventCreatedSuccessfully()
        {
            // Arrange
            User guest = TestDataGenerator.GenerateUser(1, "Alice", "Smith");
            Catalog room = TestDataGenerator.GenerateCatalog(102, RoomType.Regular);
            DateTime checkInDate = DateTime.Now;
            DateTime checkOutDate = checkInDate.AddDays(3);

            // Act
            _hotelManager.MakeBooking(guest, new State(room, 0), checkInDate, checkOutDate);

            // Assert
            var events = _dataRepository.GetAllEvents();
            Assert.IsTrue(events.Exists(e => e.Description == "Booking" && e.User == guest));
        }

    }
}




