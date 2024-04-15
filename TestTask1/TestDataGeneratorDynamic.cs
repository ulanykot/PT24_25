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

        public static Guest GenerateDynamicUser()
        {
            string firstName = "User" + userIdCounter;
            string lastName = "Test";
            int id = userIdCounter++;
            return new Guest(firstName, lastName, id);
        }

        public static State GenerateDynamicState()
        {
            Catalog roomCatalog = new Catalog(roomIdCounter++, RoomType.Regular, false);
            int price = roomIdCounter * 10;
            return new State(roomCatalog, price);
        }
    }
}
