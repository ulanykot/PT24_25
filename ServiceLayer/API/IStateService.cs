using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.API
{
    public interface IStateService
    {
        int Id { get; set; }
        int? RoomCatalogId { get; set; }
        decimal? Price { get; set; }

        #region methods
        Task AddStateAsync(int id, int roomId, int price);

        Task<IStateService> GetStateAsync(int id);

        Task UpdateStateAsync(int id, int roomId, int price);

        Task DeleteStateAsync(int id);

        Task<Dictionary<int, IStateService>> GetAllStatesAsync();

        Task<int> GetStatesCountAsync();
        #endregion

    }
}
