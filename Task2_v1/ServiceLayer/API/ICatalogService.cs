using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.API
{
    public interface ICatalogService
    {
        int Id { get; set; }
        int? RoomNumber { get; set; }
        string RoomType { get; set; }
        bool? isBooked { get; set; }

        #region methods
        Task AddCatalogAsync(int id, int roomNumber, string roomType, bool isBooked);

        Task<ICatalogService> GetCatalogAsync(int id);

        Task UpdateCatalogAsync(int id, int? roomNumber, string roomType, bool? isBooked);

        Task DeleteCatalogAsync(int id);

        Task<Dictionary<int, ICatalogService>> GetAllCatalogsAsync();

        Task<int> GetCatalogsCountAsync();

        #endregion
    }
}
