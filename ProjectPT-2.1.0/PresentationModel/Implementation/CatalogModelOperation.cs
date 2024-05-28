using Presentation.Model.API;
using Service.API;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresentationModel.Implementation;

internal class CatalogModelOperation : ICatalogModelOperation
{
    private ICatalogService _productCRUD;
    public CatalogModelOperation(ICatalogService? productCrud = null)
    {
        _productCRUD = productCrud ?? ICatalogService.CreateCatalogService();
    }

    private ICatalogModel Map(ICatalogService catalog)
    {
        return new CatalogModel(catalog.Id, catalog.RoomNumber, catalog.RoomType, catalog.isBooked);
    }
    public async Task AddAsync(int id, int roomNumber, string roomType, bool isBooked)
    {
        await _productCRUD.AddCatalogAsync(id, roomNumber, roomType, isBooked);
    }

    public async Task<ICatalogModel> GetAsync(int id)
    {
        return this.Map(await _productCRUD.GetCatalogAsync(id));
    }

    public async Task UpdateAsync(int id, int roomNumber, string roomType, bool isBooked)
    {
        await _productCRUD.UpdateCatalogAsync(id, roomNumber, roomType, isBooked);
    }

    public async Task DeleteAsync(int id)
    {
        await _productCRUD.DeleteCatalogAsync(id);
    }

    public async Task<Dictionary<int, ICatalogModel>> GetAllAsync()
    {
        Dictionary<int, ICatalogModel> result = new Dictionary<int, ICatalogModel>();

        foreach (ICatalogService Catalog in (await _productCRUD.GetAllCatalogsAsync()).Values)
        {
            result.Add(Catalog.Id, Map(Catalog));
        }

        return result;
    }

    public async Task<int> GetCountAsync()
    {
        return await _productCRUD.GetCatalogsCountAsync();
    }
}
