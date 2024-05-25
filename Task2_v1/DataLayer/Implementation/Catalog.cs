using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Implementation
{
    partial class Catalog : ICatalog
    {
        public Catalog(int id, int? roomNumber, string roomType, bool? IsBooked)
        {
            Id = id;
            RoomNumber = roomNumber;
            RoomType = roomType;
            isBooked = IsBooked;
        }

        public int Id { get; set; }
        public int? RoomNumber { get; set; }
        public string RoomType { get; set; }
        public bool? isBooked { get; set; }
    }
}
