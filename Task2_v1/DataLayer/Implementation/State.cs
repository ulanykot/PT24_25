using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Implementation
{
    internal class State : IState
    {
        public State(int id, int roomCatalogId, int price)
        {
            Id = id;
            RoomCatalogId = roomCatalogId;
            Price = price;
        }
        public int Id { get ; set ; }
        public int RoomCatalogId { get ; set ; }
        public int Price { get ; set ; }
    }
}
