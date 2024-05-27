namespace Presentation.Model.API;

public interface ICatalogModel
{
    int Id { get; set; }

    string? RoomType { get; set; }

    int? RoomNumber { get; set; }

    bool? isBooked { get; set; }
}
