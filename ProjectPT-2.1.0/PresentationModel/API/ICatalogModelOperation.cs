using System.Collections.Generic;
using System.Threading.Tasks;
using PresentationModel.Implementation;
using Service.API;

namespace Presentation.Model.API;

public interface ICatalogModelOperation
{
    static ICatalogModelOperation CreateModelOperation(ICatalogService? productCrud = null)
    {
        return new CatalogModelOperation(productCrud ?? ICatalogService.CreateCatalogService());
    }

    Task AddAsync(int id, int roomNumber, string roomType, bool isBooked);

    Task<ICatalogModel> GetAsync(int id);

    Task UpdateAsync(int id, int roomNumber, string roomType, bool isBooked);

    Task DeleteAsync(int id);

    Task<Dictionary<int, ICatalogModel>> GetAllAsync();

    Task<int> GetCountAsync();
}
