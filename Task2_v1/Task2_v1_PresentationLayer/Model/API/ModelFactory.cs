using PresentationLayer.Model.Implementation;
using ServiceLayer.API;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Model.API
{
    public static class ModelFactory
    {
        public static IEventModel CreateEventModelOperation(IEventService eventCrud = null)
        {
            return new EventModel(eventCrud ?? ServiceFactory.CreateEventService());
        }
        public static IUserModel CreateUserModelOperation(IUserService service = null)
        {
            return new UserModel(service ?? ServiceFactory.CreateUserService());
        }
        public static IStateModel CreateStateModelOperation(IStateService service = null)
        {
            return new StateModel(service ?? ServiceFactory.CreateStateService());
        }
        public static ICatalogModel CreateCatalogModelOpetation(ICatalogService catalogService = null)
        {
            return new CatalogModel(catalogService ?? ServiceFactory.CreateCatalogService());
        }
    }
}
