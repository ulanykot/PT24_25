using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTask1;

namespace TestTask1
{
    public static class TestDataGeneratorStatic
    {
        public static Guest GenerateStaticUser(int id)
        {
            string firstName = "John";
            string lastName = "Doe";
            return new Guest(firstName, lastName, id);
        }

        public static State GenerateStaticState(int roomId, Catalog roomCatalog, int price)
        {
            return new State(roomCatalog, price);
        }

        public static State GenerateStaticState(int roomId, RoomType roomType, int price)
        {
            Catalog roomCatalog = new Catalog(roomId, roomType, false);
            return new State(roomCatalog, price);
        }
    }
}