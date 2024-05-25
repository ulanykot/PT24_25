using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.API
{
    public interface IUser
    {
        int Id { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string UserType { get; set; }

    }
}
