using ServiceLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Model.Implementation
{
    internal class CatalogModel : ICatalogModel
    {
        private ICatalogService service;
        public async Task AddAsync(int id, int roomNumber, string roomType, bool isBooked)
        {
            await this.service.AddCatalogAsync(id, roomNumber, roomType, isBooked);
        }

        public async Task DeleteAsync(int id)
        {
           await this.service.DeleteCatalogAsync(id);
        }

        public async Task<Dictionary<int, ICatalogService>> GetAllAsync()
        {
            return await service.GetAllCatalogsAsync();
        }

        public async Task<ICatalogService> GetAsync(int id)
        {
            return await this.service.GetCatalogAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            return await this.service.GetCatalogsCountAsync();
        }

        public async Task UpdateAsync(int id, int? roomNumber, string roomType, bool? isBooked)
        {
            await service.UpdateCatalogAsync(id,roomNumber,roomType,isBooked);
        }
    }
}
