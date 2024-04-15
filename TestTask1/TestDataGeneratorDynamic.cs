using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTask1;

namespace TestTask1
{
    public static class TestDataGeneratorDynamic
    {
        private static int userIdCounter = 1;
        private static int roomIdCounter = 101;

        public static Guest GenerateDynamicUser(int id)
        {
            string firstName = "User" + id;
            string lastName = "Test";
            return new Guest(firstName, lastName, id);
        }

        public static State GenerateDynamicState(int roomId, RoomType roomType, int price)
        {
            Catalog roomCatalog = new Catalog(roomId, roomType, false);
            return new State(roomCatalog, price);
        }
    }
}
