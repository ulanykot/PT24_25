using Data.API;

namespace ServiceTests;

internal class FakeCatalog : ICatalog
{
    public FakeCatalog(int id, int roomNumber, string roomType, bool isBooked)
    {
        this.Id = id;
        this.RoomNumber = roomNumber;
        this.RoomType = roomType;
        this.isBooked = isBooked;
    }

    public int Id { get; set; }

    public int RoomNumber { get; set; }

    public string RoomType { get; set; }

    public bool isBooked { get; set; }
}
