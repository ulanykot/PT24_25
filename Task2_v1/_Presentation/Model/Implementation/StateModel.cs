using Presentation.Model.API;

namespace Presentation.Model.Implementation;

internal class StateModel : IStateModel
{
    public int Id { get; set; }
    public int? RoomCatalogId { get ; set ; }
    public int? Price { get ; set ; }

    public StateModel(int id, int productId, int price)
    {
        this.Id = id;
        this.RoomCatalogId = productId;
        this.Price = price;
    }
}
