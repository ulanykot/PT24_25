using System;
using DataTask1;

namespace TestTask1
{
    public static class TestDataGenerator
    {
        public static User GenerateUser(int id, string firstName, string lastName)
        {
            return new Guest(firstName, lastName, id);
        }

        public static State GenerateState(int roomId, Catalog roomCatalog, int price)
        {
            return new State(roomCatalog, price);
        }

        public static Catalog GenerateCatalog(int roomNumber, RoomType roomType, bool isBooked = false)
        {
            return new Catalog(roomNumber, roomType, isBooked);
        }

        public static BookingEvent GenerateBookingEvent(User guest, State room, DateTime checkInDate, DateTime checkOutDate)
        {
            return new BookingEvent(guest, room)
            {
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate
            };
        }
    }
}