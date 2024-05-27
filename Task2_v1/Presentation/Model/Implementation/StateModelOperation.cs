using Presentation.Model.API;
using ServiceLayer.API;
using ServiceLayer.Implementation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Model.Implementation;

internal class StateModelOperation : IStateModelOperation
{
    private IStateService _stateCrud;

    public StateModelOperation(IStateService? stateCrud = null)
    {
        this._stateCrud = stateCrud ?? ServiceFactory.CreateStateService();
    }

    private IStateModel Map(IStateService state)
    {
        return new StateModel(state.Id, (int)state.RoomCatalogId, (int)state.Price);
    }

    public async Task AddAsync(int id, int roomId, int price)
    {
        await this._stateCrud.AddStateAsync(id, roomId, price);
    }

    public async Task<IStateModel> GetAsync(int id)
    {
        return this.Map(await this._stateCrud.GetStateAsync(id));
    }

    public async Task UpdateAsync(int id, int roomId, int price)
    {
        await this._stateCrud.UpdateStateAsync(id, roomId, price);
    }

    public async Task DeleteAsync(int id)
    {
        await this._stateCrud.DeleteStateAsync(id);
    }

    public async Task<Dictionary<int, IStateModel>> GetAllAsync()
    {
        Dictionary<int, IStateModel> result = new Dictionary<int, IStateModel>();

        foreach (IStateService state in (await this._stateCrud.GetAllStatesAsync()).Values)
        {
            result.Add(state.Id, this.Map(state));
        }

        return result;
    }

    public async Task<int> GetCountAsync()
    {
        return await this._stateCrud.GetStatesCountAsync();
    }
}
