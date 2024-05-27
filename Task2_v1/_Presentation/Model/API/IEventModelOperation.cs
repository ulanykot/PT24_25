using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Presentation.Model.Implementation;
using ServiceLayer.API;
using ServiceLayer.Implementation;

namespace Presentation.Model.API;

public interface IEventModelOperation
{
    static IEventModelOperation CreateModelOperation(IEventService? eventCrud = null)
    {
        return new EventModelOperation(eventCrud ?? ServiceFactory.CreateEventService());
    }
    Task AddAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type);

    Task<IEventModel> GetAsync(int id);

    Task UpdateAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type);

    Task DeleteAsync(int id);

    Task<Dictionary<int, IEventModel>> GetAllAsync();

    Task<int> GetCountAsync();

    Task<IEnumerable<IEventModel>> GetEventsForUser(int userId);
}

