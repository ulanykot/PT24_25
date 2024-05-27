using Data.API;

namespace Data.Implementation;

internal class Event : IEvent
{
    public Event(int id, int stateId, int userId, DateTime checkIn, DateTime checkOut, string type)
    {
        Id = id;
        StateId = stateId;
        UserId = userId;
        CheckInDate = checkIn;
        CheckOutDate = checkOut;
        this.Type = type;
    }
    public int Id { get; set; }
    public int StateId { get; set; }
    public int UserId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string Type { get; set; }
}
