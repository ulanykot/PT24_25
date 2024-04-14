using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTask1
{
    public class Catalog
    {
        private int roomNumber;
        private RoomType roomType;
        private bool isBooked = false;

        public Catalog(int roomNumber, RoomType roomType, bool isBooked)
        {
            this.roomNumber = roomNumber;
            this.roomType = roomType;
            this.isBooked = isBooked;
        }
        public int RoomNumber { get => roomNumber; set => roomNumber = value; }
        public RoomType RoomType { get => roomType; set => roomType = value; }
        //public string All { get ; set => * = value; }

        public void SetIsBooked(bool value) { isBooked = value; }
    }

    public enum RoomType
    {
            Vip,
            Regular
    }
}
