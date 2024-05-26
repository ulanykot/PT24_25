using ServiceLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Model
{
    public interface ICatalogModel
    {
        Task AddAsync(int id, int roomNumber, string roomType, bool isBooked);

        Task<ICatalogService> GetAsync(int id);

        Task UpdateAsync(int id, int? roomNumber, string roomType, bool? isBooked);

        Task DeleteAsync(int id);

        Task<Dictionary<int, ICatalogService>> GetAllAsync();

        Task<int> GetCountAsync();
    }
}
