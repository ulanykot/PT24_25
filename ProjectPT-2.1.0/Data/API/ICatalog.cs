namespace Data.API;

public interface ICatalog
{
    int Id { get; set; }
    int RoomNumber { get; set; }
    string RoomType { get; set; }
    bool isBooked { get; set; }
}
