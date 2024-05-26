using ServiceLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Model
{
    public interface IStateModel
    {
        Task AddAsync(int id, int roomId, int price);

        Task<IStateService> GetAsync(int id);

        Task UpdateAsync(int id, int roomId, int price);

        Task DeleteAsync(int id);

        Task<Dictionary<int, IStateService>> GetAllAsync();

        Task<int> GetCountAsync();
    }
}
