using Service.API;

namespace PresentationTests;

internal class FakeCatalogService : ICatalogService
{
    private readonly FakeDataRepository _fakeRepository = new FakeDataRepository();

    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public string RoomType { get; set; }
    public bool isBooked { get; set; }

    public FakeCatalogService(int id, int roomNumber, string roomType, bool isBooked)
    {
        this.Id = id;
        this.RoomNumber = roomNumber;
        this.RoomType = roomType;
        this.isBooked = isBooked;
    }

    public async Task AddCatalogAsync(int id, int roomNumber, string roomType, bool isBooked)
    {
        await this._fakeRepository.AddCatalogAsync(id, roomNumber, roomType, isBooked);
    }

    public async Task<ICatalogService> GetCatalogAsync(int id)
    {
        return await this._fakeRepository.GetCatalogAsync(id);
    }

    public async Task UpdateCatalogAsync(int id, int roomNumber, string roomType, bool isBooked)
    {
        await this._fakeRepository.UpdateCatalogAsync(id, roomNumber, roomType, isBooked);
    }

    public async Task DeleteCatalogAsync(int id)
    {
        await this._fakeRepository.DeleteCatalogAsync(id);
    }

    public async Task<Dictionary<int, ICatalogService>> GetAllCatalogsAsync()
    {
        Dictionary<int, ICatalogService> result = new Dictionary<int, ICatalogService>();

        foreach (ICatalogService catalog in (await this._fakeRepository.GetAllCatalogAsync()).Values)
        {
            result.Add(catalog.Id, catalog);
        }

        return result;
    }

    public async Task<int> GetCatalogsCountAsync()
    {
        return await this._fakeRepository.GetCatalogsCountAsync();
    }
}
