namespace Data.API;

public interface IEvent
{
     int Id { get; set; }

        int StateId { get; set; }

        int UserId { get; set; }

        DateTime CheckInDate { get; set; }

        DateTime CheckOutDate { get; set; }

        string Type { get; set; }
}
