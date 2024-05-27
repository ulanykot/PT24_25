namespace Presentation.Model.API;

public interface IStateModel
{
    int Id { get; set; }
    int? RoomCatalogId { get; set; }
    int? Price { get; set; }

}
