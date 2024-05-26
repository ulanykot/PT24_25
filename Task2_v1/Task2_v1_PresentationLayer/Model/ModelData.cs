using ServiceLayer.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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

        public ModelData() : this(new UserService(), new CatalogService(), new EventService(), new StateService()) { }

        public override void AddUser(User user)
        {
            _userService.AddUser(user);
            OnDataChanged();
        }

        public override User GetUser(int id) => _userService.GetUser(id);

        public override void RemoveUser(int id)
        {
            _userService.DeleteUser(id);
            OnDataChanged();
        }

        public override IEnumerable<User> GetAllUsers() => _userService.GetAllUsers();

        public override void AddCatalog(Catalog catalog)
        {
            _catalogService.AddCatalog(catalog);
            OnDataChanged();
        }

        public override Catalog GetCatalog(int id) => _catalogService.GetCatalogById(id);

        public override IEnumerable<Catalog> GetAllCatalogs() => _catalogService.GetAllCatalogs();

        public override void RemoveCatalog(int id)
        {
            _catalogService.DeleteCatalog(id);
            OnDataChanged();
        }

        public override void AddEvent(Event eve)
        {
            _eventService.AddEvent(eve);
            OnDataChanged();
        }

        public override Event GetEvent(int id) => _eventService.GetEventById(id);

        public override IEnumerable<Event> GetAllEvents() => _eventService.GetAllEvents();

        public override void RemoveEvent(int id)
        {
            _eventService.DeleteEvent(id);
            OnDataChanged();
        }

        public override void AddState(State state)
        {
            _stateService.AddState(state);
            OnDataChanged();
        }

        public override State GetState(int id) => _stateService.GetState(id);

        public override IEnumerable<State> GetAllStates() => _stateService.GetAllStates();

        public override void RemoveState(int id)
        {
            _stateService.DeleteState(id);
            OnDataChanged();
        }

        public override Task<IEnumerable<Event>> GetEventsForUser(int userId)
        {
            return _userService.GetEventsForUser(userId);
        }
    }
}

