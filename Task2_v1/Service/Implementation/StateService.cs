using DataLayer.API;
using DataLayer.Implementation;
using ServiceLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
    internal class StateService : IStateService
    {
        IDataRepository _repository;
        public StateService(int id, int? roomId, int? price)
        {
            this.Id = id;
            this.Price = price;
            this.RoomCatalogId = roomId;
        }
        public StateService(IDataRepository repository)
        {
             this._repository = repository;
        }

        public int Id { get; set; }
        public int? RoomCatalogId { get; set; }
        public int? Price { get; set; }

        private IStateService Map(IState state)
        {
            return new StateService(state.Id, state.RoomCatalogId, state.Price);
        }
        public async Task AddStateAsync(int id, int roomId, int price)
        {
            await _repository.AddStateAsync(id, roomId, price);
        }

        public async Task DeleteStateAsync(int id)
        {
            await this._repository.DeleteStateAsync(id);
        }

        public async Task<Dictionary<int, IStateService>> GetAllStatesAsync()
        {
            Dictionary<int, IStateService> result = new Dictionary<int, IStateService>();

            foreach (IState state in (await this._repository.GetAllStatesAsync()).Values)
            {
                result.Add(state.Id, this.Map(state));
            }

            return result;
        }

        public async Task<IStateService> GetStateAsync(int id)
        {
            return this.Map(await this._repository.GetStateAsync(id));
        }

        public async Task<int> GetStatesCountAsync()
        {
            return await this._repository.GetStatesCountAsync();
        }

        public async Task UpdateStateAsync(int id, int roomId, int price)
        {
            await this._repository.UpdateStateAsync(id, roomId, price);
        }

    }
}
