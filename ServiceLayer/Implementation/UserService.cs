using DataLayer.API;
using DataLayer.Implementation;
using ServiceLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceLayer.Implementation
{
    internal class UserService : IUserService
    {
        private IDataRepository _repository;

        public UserService(IDataRepository repository)
        {
            _repository = repository;
        }
        public UserService(int id, string firstName, string lastName, string UserType)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserType = UserType;

        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }
               
        public IUserService Map(IUser user)
        {
            return new UserService(user.Id, user.FirstName, user.LastName, user.UserType);
        }
        public async Task AddUserAsync(int id, string firstName, string lastName, string userType)
        {
            await this._repository.AddUserAsync(id, firstName, lastName, userType);
        }

        public async Task DeleteUserAsync(int id)
        {
            await this._repository.DeleteUserAsync(id);
        }

        public async Task<Dictionary<int, IUserService>> GetAllUsersAsync()
        {
            Dictionary<int, IUserService> result = new Dictionary<int, IUserService>();

            foreach (IUser user in (await this._repository.GetAllUsersAsync()).Values)
            {
                result.Add(user.Id, this.Map(user));
            }

            return result;
        }

        public async Task<IUserService> GetUserAsync(int id)
        {
            return this.Map(await this._repository.GetUserAsync(id));
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await this._repository.GetUsersCountAsync();
        }

        public async Task UpdateUserAsync(int id, string firstName, string lastName, string userType)
        {
            await this._repository.UpdateUserAsync(id, firstName, lastName, userType);
        }
    }
}
