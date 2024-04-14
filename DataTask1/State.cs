using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTask1
{
    public class State
    {
        private Catalog roomCatalog;
        private int price;

        public State(Catalog roomCatalog, int price)
        {
            this.roomCatalog = roomCatalog;
            this.price = price;
        }

        public Catalog RoomCatalog { get { return roomCatalog; } set => roomCatalog = value; }
        public int Price { get => price; set => price = value; }
    }
}
