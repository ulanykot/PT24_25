using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTask1
{
    public abstract class User
    {
        private string firstName;
        private string lastName;
        private int id;
        public abstract string UserType { get; }

        protected User(string firstName, string lastName, int id)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.id = id;
        }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int Id { get => id; set => id = value; }
        public string All { get => id + " " + firstName + " " + lastName; }
    }
    public class Guest : User
    {
        public override string UserType => "Guest";
        public Guest(string firstName, string lastName, int id) : base(firstName, lastName, id)
        {
        }
    }

    public class Staff : User
    {
        public override string UserType => "Staff";
        public Staff(string firstName, string lastName, int id) : base(firstName, lastName, id)
        {
        }
    }


}
