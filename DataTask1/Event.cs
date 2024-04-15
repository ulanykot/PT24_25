using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTask1
{
    public abstract class Event
    {
        public abstract string Description { get; }
        public User guest { get; set; }
        public State room { get; set; }

        public User User { get => guest; set => guest = value; }
        public State Room { get => room; set => room = value; }

        public Event(User guest, State room)
        {
            this.guest = guest;
            this.room = room;
        }
    }
    public class BookingEvent : Event
    {
        public override string Description => "Booking";        
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public BookingEvent(User guest, State room) : base(guest, room) 
        { 
        }

    }

    public class CheckInEvent : Event
    {
        public override string Description => "Check-In";      
        public DateTime CheckInDate { get; set; }
        public CheckInEvent(User guest, State room) : base(guest, room)
        {
        }

    }

    public class CheckOutEvent : Event
    {
        public override string Description => "Check-Out";
        public DateTime CheckOutDate { get; set; }
        public CheckOutEvent(User guest, State room) : base(guest, room)
        {
        }


    }

}
