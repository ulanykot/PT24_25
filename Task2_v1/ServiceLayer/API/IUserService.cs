using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.API
{
    internal interface IUserService
    {


        Task AddUserAsync(int id, string firstName, string lastName, string userType);

        Task<IUser> GetUserAsync(int id);

        Task UpdateUserAsync(int id, string firstName, string lastName, string userType);

        Task DeleteUserAsync(int id);

        Task<Dictionary<int, IUserService>> GetAllUsersAsync();

        Task<int> GetUsersCountAsync();
    }
}
