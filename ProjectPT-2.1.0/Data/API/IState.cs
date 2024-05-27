namespace Data.API;

public interface IState
{
    int Id { get; set; }
    int RoomCatalogId { get; set; }
    int Price { get; set; }
}
