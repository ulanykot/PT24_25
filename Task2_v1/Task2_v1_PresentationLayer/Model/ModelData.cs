using ServiceLayer;
using System.Collections.Generic;
using DataLayer;

namespace Task2_v1_PresentationLayer.Model
{

    internal class ModelData : ModelDataAPI
    {
        private readonly IUserService _userService;
        private readonly ICatalogService _catalogService;
        private readonly IEventService _eventService;
        private readonly IStateService _stateService;

        public ModelData(IUserService userService, ICatalogService catalogService, IEventService eventService, IStateService stateService)
        {
            _userService = userService;
            _catalogService = catalogService;
            _eventService = eventService;
            _stateService = stateService;
        }

        public ModelData()
        {
            
        }

        // Implementing the abstract methods
        public override void AddUser(User user) => _userService.AddUser(user);
        public override User GetUser(int id) => _userService.GetUser(id);
        public override void RemoveUser(int id) => _userService.DeleteUser(id);
        public override IEnumerable<User> GetAllUsers() => _userService.GetAllUsers();

        public override void AddCatalog(Catalog catalog) => _catalogService.AddCatalog(catalog);
        public override Catalog GetCatalog(int id) => _catalogService.GetCatalogById(id);
        public override IEnumerable<Catalog> GetAllCatalogs() => _catalogService.GetAllCatalogs();
        public override void RemoveCatalog(int id) => _catalogService.DeleteCatalog(id);

        public override void AddEvent(Event eve) => _eventService.AddEvent(eve);
        public override Event GetEvent(int id) => _eventService.GetEventById(id);
        public override IEnumerable<Event> GetAllEvents() => _eventService.GetAllEvents();
        public override void RemoveEvent(int id) => _eventService.DeleteEvent(id);

        public override void AddState(State state) => _stateService.AddState(state);
        public override State GetState(int id) => _stateService.GetState(id);
        public override IEnumerable<State> GetAllStates() => _stateService.GetAllStates();
        public override void RemoveState(int id) => _stateService.DeleteState(id);


    }
}
