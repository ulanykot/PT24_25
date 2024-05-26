using ServiceLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Model
{
    public interface IUserModel
    {
         Task AddAsync(int id, string firstName, string lastName, string userType);

         Task<IUserService> GetAsync(int id);

         Task UpdateAsync(int id, string firstName, string lastName, string userType);

         Task DeleteAsync(int id);

         Task<Dictionary<int, IUserService>> GetAllAsync();

         Task<int> GetCountAsync();
    }
}
