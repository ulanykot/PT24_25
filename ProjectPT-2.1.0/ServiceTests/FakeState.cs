using Data.API;

namespace ServiceTests;

internal class FakeState : IState
{
    public FakeState(int id, int roomCatalogId, int price)
    {
        this.Id = id;
        this.RoomCatalogId = roomCatalogId;
        this.Price = price;
    }

    public int Id { get; set; }

    public int RoomCatalogId { get; set; }

    public int Price { get; set; }
}
