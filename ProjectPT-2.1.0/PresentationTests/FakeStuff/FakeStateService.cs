using Service.API;

namespace PresentationTests;

internal class FakeStateService : IStateService
{
    private readonly FakeDataRepository _fakeRepository = new FakeDataRepository();

    public int Id { get; set; }
    public int Price { get; set; }
    public int RoomCatalogId { get;  set; }

    public FakeStateService(int id, int roomId, int price)
    {
        this.Id = id;
        this.Price = price;
        this.RoomCatalogId = roomId;
    }

    public FakeStateService()
    {

    }

    public async Task AddStateAsync(int id, int roomCatalogId, int price)
    {
        await _fakeRepository.AddStateAsync(id, roomCatalogId, price);
    }

    public async Task<IStateService> GetStateAsync(int id)
    {
        return await this._fakeRepository.GetStateAsync(id);
    }

    public async Task UpdateStateAsync(int id, int roomCatalogId, int price)
    {
        await this._fakeRepository.UpdateStateAsync(id, roomCatalogId, price);
    }

    public async Task DeleteStateAsync(int id)
    {
        await this._fakeRepository.DeleteStateAsync(id);
    }

    public async Task<Dictionary<int, IStateService>> GetAllStatesAsync()
    {
        Dictionary<int, IStateService> result = new Dictionary<int, IStateService>();

        foreach (IStateService state in (await this._fakeRepository.GetAllStatesAsync()).Values)
        {
            result.Add(state.Id, state);
        }

        return result;
    }

    public async Task<int> GetStatesCountAsync()
    {
        return await this._fakeRepository.GetStatesCountAsync();
    }

}
