using System.Collections.Generic;
using System.Threading.Tasks;
using Presentation.Model.Implementation;
using ServiceLayer.API;
using ServiceLayer.Implementation;

namespace Presentation.Model.API;

public interface IStateModelOperation
{
    static IStateModelOperation CreateModelOperation(IStateService? stateCrud = null)
    {
        return new StateModelOperation(stateCrud ?? ServiceFactory.CreateStateService());
    }

    Task AddAsync(int id, int roomId, int price);

    Task<IStateModel> GetAsync(int id);

    Task UpdateAsync(int id, int roomId, int price);

    Task DeleteAsync(int id);

    Task<Dictionary<int, IStateModel>> GetAllAsync();

    Task<int> GetCountAsync();
}
