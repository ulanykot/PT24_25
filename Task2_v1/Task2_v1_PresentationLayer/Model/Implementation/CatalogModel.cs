using ServiceLayer.API;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Model.Implementation
{
    internal class CatalogModel : ICatalogModel
    {
        private ICatalogService _service;
        public CatalogModel(ICatalogService service = null)
        {
           _service = service ?? ServiceFactory.CreateCatalogService();
        }
        public async Task AddAsync(int id, int roomNumber, string roomType, bool isBooked)
        {
            await this._service.AddCatalogAsync(id, roomNumber, roomType, isBooked);
        }

        public async Task DeleteAsync(int id)
        {
           await this._service.DeleteCatalogAsync(id);
        }

        public async Task<Dictionary<int, ICatalogService>> GetAllAsync()
        {
            return await _service.GetAllCatalogsAsync();
        }

        public async Task<ICatalogService> GetAsync(int id)
        {
            return await this._service.GetCatalogAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            return await this._service.GetCatalogsCountAsync();
        }

        public async Task UpdateAsync(int id, int? roomNumber, string roomType, bool? isBooked)
        {
            await _service.UpdateCatalogAsync(id,roomNumber,roomType,isBooked);
        }
    }
}
