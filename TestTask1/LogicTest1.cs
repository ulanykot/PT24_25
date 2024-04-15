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
            User user = TestDataGeneratorStatic.GenerateStaticUser(1);

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
            State roomState = TestDataGeneratorStatic.GenerateStaticState(101, RoomType.Vip, 100);
            Catalog room = roomState.RoomCatalog;

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
            User guest = TestDataGeneratorStatic.GenerateStaticUser(1);
            Catalog room = TestDataGeneratorStatic.GenerateStaticState(102, RoomType.Regular, 100).RoomCatalog;
            DateTime checkInDate = DateTime.Now;
            DateTime checkOutDate = checkInDate.AddDays(3);

            // Act
            _hotelManager.MakeBooking(guest, new State(room, 0), checkInDate, checkOutDate);

            // Assert
            var events = _dataRepository.GetAllEvents();
            Assert.IsTrue(events.Exists(e => e.Description == "Booking" && e.User == guest));
        }

        [TestMethod]
        public void AddUser_Dynamic_ValidUser_UserAddedSuccessfully()
        {
            // Arrange
            User user = TestDataGeneratorDynamic.GenerateDynamicUser(1);

            // Act
            _hotelManager.AddUser(user);

            // Assert
            var users = _dataRepository.GetAllUsers();
            Assert.IsTrue(users.Contains(user));
        }

        [TestMethod]
        public void AddRoomToCatalog_Dynamic_ValidRoom_RoomAddedSuccessfully()
        {
            // Arrange
            State roomState = TestDataGeneratorDynamic.GenerateDynamicState(101, RoomType.Vip, 100);
            Catalog room = roomState.RoomCatalog;

            // Act
            _hotelManager.AddRoomToCatalog(101, room);

            // Assert
            var rooms = _dataRepository.GetAllStates();
            Assert.IsTrue(rooms.Exists(r => r.RoomCatalog == room));
        }

        [TestMethod]
        public void MakeBooking_Dynamic_ValidBooking_BookingEventCreatedSuccessfully()
        {
            // Arrange
            User guest = TestDataGeneratorDynamic.GenerateDynamicUser(1);
            Catalog room = TestDataGeneratorDynamic.GenerateDynamicState(102, RoomType.Regular, 100).RoomCatalog;
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
