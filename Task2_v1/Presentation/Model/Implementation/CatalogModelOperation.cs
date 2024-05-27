using DataLayer.Database;
using Presentation.Model.API;
using ServiceLayer.API;
using ServiceLayer.Implementation;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Presentation.Model.Implementation;

internal class CatalogModelOperation : ICatalogModelOperation
{
    private ICatalogService _productCRUD;

    public CatalogModelOperation(ICatalogService? productCrud = null)
    {
        this._productCRUD = productCrud ?? ServiceFactory.CreateCatalogService();
    }

    private ICatalogModel Map(ICatalogService catalog)
    {
        return new CatalogModel(catalog.Id, catalog.RoomNumber, catalog.RoomType, catalog.isBooked);
    }
    public async Task AddAsync(int id, int roomNumber, string roomType, bool isBooked)
    {
        await this._productCRUD.AddCatalogAsync(id, roomNumber, roomType, isBooked);
    }

    public async Task<ICatalogModel> GetAsync(int id)
    {
        return this.Map(await this._productCRUD.GetCatalogAsync(id));
    }

    public async Task UpdateAsync(int id, int? roomNumber, string roomType, bool? isBooked)
    {
        await this._productCRUD.UpdateCatalogAsync(id, roomNumber, roomType, isBooked);
    }

    public async Task DeleteAsync(int id)
    {
        await this._productCRUD.DeleteCatalogAsync(id);
    }

    public async Task<Dictionary<int, ICatalogModel>> GetAllAsync()
    {
        Dictionary<int, ICatalogModel> result = new Dictionary<int, ICatalogModel>();

        foreach (ICatalogService Catalog in (await this._productCRUD.GetAllCatalogsAsync()).Values)
        {
            result.Add(Catalog.Id, this.Map(Catalog));
        }

        return result;
    }

    public async Task<int> GetCountAsync()
    {
        return await this._productCRUD.GetCatalogsCountAsync();
    }
}
