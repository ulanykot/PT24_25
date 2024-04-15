using System;
using DataTask1;

namespace LogicTask1
{
    public class HotelManager
    {
        private readonly IDataRepository _dataRepository;

        public HotelManager(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        // Method to add a user
        public void AddUser(User user)
        {
            _dataRepository.AddUser(user);
        }

        // Method to add a room to the catalog
        public void AddRoomToCatalog(int id, Catalog room)
        {
            var state = new State(room, 0); 
            _dataRepository.AddState(state);
        }

        // Method to make a booking
        public void MakeBooking(User guest, State room, DateTime checkInDate, DateTime checkOutDate)
        {
            var bookingEvent = new BookingEvent(guest, room)
            {
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate
            };
            _dataRepository.AddEvent(bookingEvent);
        }

        // Method to check in a guest
        public void CheckInGuest(User guest, State room, DateTime checkInDate)
        {
            var checkInEvent = new CheckInEvent(guest, room)
            {
                CheckInDate = checkInDate
            };
            _dataRepository.AddEvent(checkInEvent);
        }

        // Method to check out a guest
        public void CheckOutGuest(User guest, State room, DateTime checkOutDate)
        {
            var checkOutEvent = new CheckOutEvent(guest, room)
            {
                CheckOutDate = checkOutDate
            };
            _dataRepository.AddEvent(checkOutEvent);
        }
    }
}
