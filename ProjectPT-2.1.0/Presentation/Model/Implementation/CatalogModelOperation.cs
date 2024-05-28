using Presentation.Model.API;
using Service.API;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Model.Implementation;

internal class CatalogModelOperation : ICatalogModelOperation
{
    private ICatalogService _catalogCRUD;
    public CatalogModelOperation(ICatalogService? catalogCrud = null)
    {
        this._catalogCRUD = catalogCrud ?? ICatalogService.CreateCatalogService();
    }

    private ICatalogModel Map(ICatalogService catalog)
    {
        return new CatalogModel(catalog.Id, catalog.RoomNumber, catalog.RoomType, catalog.isBooked);
    }
    public async Task AddAsync(int id, int roomNumber, string roomType, bool isBooked)
    {
        await this._catalogCRUD.AddCatalogAsync(id, roomNumber, roomType, isBooked);
    }

    public async Task<ICatalogModel> GetAsync(int id)
    {
        return this.Map(await this._catalogCRUD.GetCatalogAsync(id));
    }

    public async Task UpdateAsync(int id, int roomNumber, string roomType, bool isBooked)
    {
        await this._catalogCRUD.UpdateCatalogAsync(id, roomNumber, roomType, isBooked);
    }

    public async Task DeleteAsync(int id)
    {
        await this._catalogCRUD.DeleteCatalogAsync(id);
    }

    public async Task<Dictionary<int, ICatalogModel>> GetAllAsync()
    {
        Dictionary<int, ICatalogModel> result = new Dictionary<int, ICatalogModel>();

        foreach (ICatalogService Catalog in (await this._catalogCRUD.GetAllCatalogsAsync()).Values)
        {
            result.Add(Catalog.Id, this.Map(Catalog));
        }

        return result;
    }

    public async Task<int> GetCountAsync()
    {
        return await this._catalogCRUD.GetCatalogsCountAsync();
    }
}
