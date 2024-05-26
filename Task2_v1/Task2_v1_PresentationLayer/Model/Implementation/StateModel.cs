using ServiceLayer.API;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Model.Implementation
{
    internal class StateModel : IStateModel
    {
        IStateService _service;
        public StateModel(IStateService service = null)
        {
            _service = service ?? ServiceFactory.CreateStateService();
        }
        public async Task AddAsync(int id, int roomId, int price)
        {
           await _service.AddStateAsync(id, roomId, price); 
        }

        public async Task DeleteAsync(int id)
        {
            await _service.DeleteStateAsync(id);
        }

        public async Task<Dictionary<int, IStateService>> GetAllAsync()
        {
            return await _service.GetAllStatesAsync();
        }

        public async Task<IStateService> GetAsync(int id)
        {
           return await _service.GetStateAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
           return await _service.GetStatesCountAsync();
        }

        public async Task UpdateAsync(int id, int roomId, int price)
        {
            await _service.UpdateStateAsync(id, roomId, price);
        }
    }
}
