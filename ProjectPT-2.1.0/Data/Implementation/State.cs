using Data.API;

namespace Data.Implementation;

internal class State : IState
{
    public State(int id, int roomCatalogId, int price)
    {
        Id = id;
        RoomCatalogId = roomCatalogId;
        Price = price;
    }
    public int Id { get; set; }
    public int RoomCatalogId { get; set; }
    public int Price { get; set; }
}
