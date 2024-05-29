using PresentationModel;
using Service.API;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresentationModel.Implementation;

internal class StateModelOperation : IStateModelOperation
{
    private IStateService _stateCrud;

    public StateModelOperation(IStateService? stateCrud = null)
    {
        _stateCrud = stateCrud ?? IStateService.CreateStateService();
    }

    private IStateModel Map(IStateService state)
    {
        return new StateModel(state.Id, (int)state.RoomCatalogId, (int)state.Price);
    }

    public async Task AddAsync(int id, int roomId, int price)
    {
        await _stateCrud.AddStateAsync(id, roomId, price);
    }

    public async Task<IStateModel> GetAsync(int id)
    {
        return this.Map(await _stateCrud.GetStateAsync(id));
    }

    public async Task UpdateAsync(int id, int roomId, int price)
    {
        await _stateCrud.UpdateStateAsync(id, roomId, price);
    }

    public async Task DeleteAsync(int id)
    {
        await _stateCrud.DeleteStateAsync(id);
    }

    public async Task<Dictionary<int, IStateModel>> GetAllAsync()
    {
        Dictionary<int, IStateModel> result = new Dictionary<int, IStateModel>();

        foreach (IStateService state in (await _stateCrud.GetAllStatesAsync()).Values)
        {
            result.Add(state.Id, Map(state));
        }

        return result;
    }

    public async Task<int> GetCountAsync()
    {
        return await _stateCrud.GetStatesCountAsync();
    }
}
