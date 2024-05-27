using Data.API;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface ICatalogService
    {
        int Id { get; set; }
        int RoomNumber { get; set; }
        string RoomType { get; set; }
        bool isBooked { get; set; }

        static ICatalogService CreateCatalogService(IDataRepository? dataRepository = null)
        {
            return new CatalogService(dataRepository ?? IDataRepository.CreateDatabase());
        }

        #region methods
        Task AddCatalogAsync(int id, int roomNumber, string roomType, bool isBooked);

        Task<ICatalogService> GetCatalogAsync(int id);

        Task UpdateCatalogAsync(int id, int roomNumber, string roomType, bool isBooked);

        Task DeleteCatalogAsync(int id);

        Task<Dictionary<int, ICatalogService>> GetAllCatalogsAsync();

        Task<int> GetCatalogsCountAsync();

        #endregion
    }
}
