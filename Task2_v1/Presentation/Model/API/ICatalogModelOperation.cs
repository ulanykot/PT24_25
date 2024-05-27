using System.Collections.Generic;
using System.Threading.Tasks;
using Presentation.Model.Implementation;
using ServiceLayer.API;
using ServiceLayer.Implementation;

namespace Presentation.Model.API;

public interface ICatalogModelOperation
{
    static ICatalogModelOperation CreateModelOperation(ICatalogService? productCrud = null)
    {
        return new CatalogModelOperation(productCrud ?? ServiceFactory.CreateCatalogService());
    }

    Task AddAsync(int id, int roomNumber, string roomType, bool isBooked);

    Task<ICatalogModel> GetAsync(int id);

    Task UpdateAsync(int id, int roomNumber, string roomType, bool isBooked);

    Task DeleteAsync(int id);

    Task<Dictionary<int, ICatalogModel>> GetAllAsync();

    Task<int> GetCountAsync();
}
