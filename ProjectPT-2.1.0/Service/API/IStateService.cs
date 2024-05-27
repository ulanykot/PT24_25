using Data.API;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IStateService
    {
        static IStateService CreateStateService(IDataRepository? dataRepository = null)
        {
            return new StateService(dataRepository ?? IDataRepository.CreateDatabase());
        }
        int Id { get; set; }
        int RoomCatalogId { get; set; }
        int Price { get; set; }

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
