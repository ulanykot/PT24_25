using System.Collections.Generic;
using System.Threading.Tasks;
using PresentationModel.Implementation;
using Service.API;

namespace PresentationModel;

public interface IStateModelOperation
{
    static IStateModelOperation CreateModelOperation(IStateService? stateCrud = null)
    {
        return new StateModelOperation(stateCrud ?? IStateService.CreateStateService());
    }

    Task AddAsync(int id, int roomId, int price);

    Task<IStateModel> GetAsync(int id);

    Task UpdateAsync(int id, int roomId, int price);

    Task DeleteAsync(int id);

    Task<Dictionary<int, IStateModel>> GetAllAsync();

    Task<int> GetCountAsync();
}
