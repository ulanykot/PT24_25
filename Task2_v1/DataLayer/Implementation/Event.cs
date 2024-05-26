using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Implementation
{
    partial class Event : IEvent
    {
        public Event(int id, int? stateId, int? userId, DateTime? checkIn, DateTime? checkOut, string type)
        {
            Id = id;
            StateId = stateId;
            UserId = userId;
            CheckInDate = checkIn;
            CheckOutDate = checkOut;
            this.Type = type;
        }
        public int Id { get; set; }
        public int? StateId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string Type { get; set; }
    }
}
