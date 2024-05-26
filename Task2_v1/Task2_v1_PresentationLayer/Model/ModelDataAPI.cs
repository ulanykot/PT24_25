using ServiceLayer.API;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Task2_v1_PresentationLayer.Model
{
    public abstract class ModelDataAPI
    {
        #region creation
        public static ModelDataAPI Create() => new ModelData();
        public static IEventService ModelEventCreate()
        {
            return ServiceFactory.CreateEventService();
        }
        public static IUserService ModelUserCreate()
        {
            return ServiceFactory.CreateUserService();
        }
        public static IStateService ModelStateCreate()
        {
            return ServiceFactory.CreateStateService();
        }
        public static ICatalogService ModelCatalogCreate()
        {
            return ServiceFactory.CreateCatalogService();
        }
        #endregion


}

