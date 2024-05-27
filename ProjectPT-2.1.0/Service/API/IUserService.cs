using Data.API;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IUserService
    {
        static IUserService CreateUserService(IDataRepository? dataRepository = null)
        {
            return new UserService(dataRepository ?? IDataRepository.CreateDatabase());
        }
        int Id { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string UserType { get; set; }

        Task AddUserAsync(int id, string firstName, string lastName, string userType);

        Task<IUserService> GetUserAsync(int id);

        Task UpdateUserAsync(int id, string firstName, string lastName, string userType);

        Task DeleteUserAsync(int id);

        Task<Dictionary<int, IUserService>> GetAllUsersAsync();

        Task<int> GetUsersCountAsync();

    }
}
