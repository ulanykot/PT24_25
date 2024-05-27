using DataLayer.API;
using DataLayer.Implementation;
using ServiceLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
    public static class ServiceFactory
    {
        public static ICatalogService CreateCatalogService(IDataRepository repository = null)
        {
            return new CatalogService(repository ?? IDataRepository.CreateDatabase());
        }
        public static IEventService CreateEventService(IDataRepository repository = null)
        {
            return new EventService(repository ?? IDataRepository.CreateDatabase());
        }
        public static IStateService CreateStateService(IDataRepository repository = null)
        {
            return new StateService(repository ?? IDataRepository.CreateDatabase());
        }
        public static IUserService CreateUserService(IDataRepository repository = null)
        {
            return new UserService(repository ?? IDataRepository.CreateDatabase());
        }
    }
}
