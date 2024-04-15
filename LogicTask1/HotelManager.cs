using DataTask1;

namespace LogicTask1
{
    public class HotelManager
    {

        private DataRepository _dataRepository;

        public HotelManager(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void AddUser(User user)
        {
            _dataRepository.AddUser(user);
        }

        public void AddRoomToCatalog(int id, Catalog room)
        {
            var state = new State(room, 0); 
            _dataRepository.AddState(state);
        }

        public void MakeBooking(User guest, State room, DateTime checkInDate, DateTime checkOutDate)
        {
            var bookingEvent = new BookingEvent(guest, room)
            {
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate
            };
            _dataRepository.AddEvent(bookingEvent);
        }

        public void CheckInGuest(User guest, State room, DateTime checkInDate)
        {
            var checkInEvent = new CheckInEvent(guest, room)
            {
                CheckInDate = checkInDate
            };
            _dataRepository.AddEvent(checkInEvent);
        }

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
