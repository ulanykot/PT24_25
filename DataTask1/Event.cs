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
        public required User guest { get; set; }
        public required State room { get; set; }

    }
    public class BookingEvent : Event
    {
        public override string Description => "Booking";        
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

    }

    public class CheckInEvent : Event
    {
        public override string Description => "Check-In";      
        public DateTime CheckInDate { get; set; }

    }

    public class CheckOutEvent : Event
    {
        public override string Description => "Check-Out";
        public DateTime CheckOutDate { get; set; }

    }

}
