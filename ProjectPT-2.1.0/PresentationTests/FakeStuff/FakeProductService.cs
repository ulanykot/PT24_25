using Service.API;

namespace PresentationTests;

internal class FakeProductService : ICatalogService
{
    private readonly FakeDataRepository _fakeRepository = new FakeDataRepository();

    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public string RoomType { get; set; }
    public bool isBooked { get; set; }

    public FakeProductService()
    {

    }

    public async Task AddProductAsync(int id, int roomNumber, string roomType, bool isBooked)
    {
        await this._fakeRepository.AddProductAsync(id, roomNumber, roomType, isBooked);
    }

    public async Task<ICatalogService> GetProductAsync(int id)
    {
        return await this._fakeRepository.GetProductAsync(id);
    }

    public async Task UpdateProductAsync(int id, string name, double price)
    {
        await this._fakeRepository.UpdateProductAsync(id, name, price);
    }

    public async Task DeleteProductAsync(int id)
    {
        await this._fakeRepository.DeleteProductAsync(id);
    }

    public async Task<Dictionary<int, ICatalogService>> GetAllProductsAsync()
    {
        Dictionary<int, ICatalogService> result = new Dictionary<int, ICatalogService>();

        foreach (ICatalogService product in (await this._fakeRepository.GetAllProductsAsync()).Values)
        {
            result.Add(product.Id, product);
        }

        return result;
    }

    public async Task<int> GetProductsCountAsync()
    {
        return await this._fakeRepository.GetProductsCountAsync();
    }

    public Task AddCatalogAsync(int id, int roomNumber, string roomType, bool isBooked)
    {
        throw new NotImplementedException();
    }

    public Task<ICatalogService> GetCatalogAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCatalogAsync(int id, int roomNumber, string roomType, bool isBooked)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCatalogAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Dictionary<int, ICatalogService>> GetAllCatalogsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCatalogsCountAsync()
    {
        throw new NotImplementedException();
    }
}
