using Presentation.Model.API;
using System;

namespace Presentation.Model.Implementation;

internal class EventModel : IEventModel
{
    public int Id { get; set; }
    public int StateId { get; set; }
    public int UserId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string Type { get; set; }

    public EventModel(int id, int stateId, int userId, DateTime checkIn, DateTime checkOut, string type)
    {
        this.Id = id;
        this.StateId = stateId;
        this.UserId = userId;
        this.CheckInDate = checkIn;
        this.CheckOutDate = checkOut;
        this.Type = type;
    }
}
