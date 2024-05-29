using PresentationModel;

namespace PresentationModel.Implementation;

internal class StateModel : IStateModel
{
    public int Id { get; set; }
    public int RoomCatalogId { get; set; }
    public int Price { get; set; }

    public StateModel(int id, int productId, int price)
    {
        Id = id;
        RoomCatalogId = productId;
        Price = price;
    }
}
