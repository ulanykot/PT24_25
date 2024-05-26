using ServiceLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Model.Implementation
{
    internal class UserModel : IUserModel
    {
        IUserService _service;
        public async Task AddAsync(int id, string firstName, string lastName, string userType)
        {
            await _service.AddUserAsync(id, firstName, lastName, userType);
        }

        public async Task DeleteAsync(int id)
        {
            await _service.DeleteUserAsync(id);
        }

        public async Task<Dictionary<int, IUserService>> GetAllAsync()
        {
            return await _service.GetAllUsersAsync();
        }

        public async Task<IUserService> GetAsync(int id)
        {
           return await _service.GetUserAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _service.GetUsersCountAsync();
        }

        public async Task UpdateAsync(int id, string firstName, string lastName, string userType)
        {
            await _service.UpdateUserAsync(id, firstName, lastName, userType);
        }
    }
}
