using Data.API;
using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    internal class CatalogService : ICatalogService
    {
        IDataRepository _repository;
        public CatalogService(int id, int roomNumber, string roomType, bool isBooked)
        {
            this.Id = id;
            this.RoomNumber = roomNumber;
            this.RoomType = roomType;
            this.isBooked = isBooked;
        }
        public CatalogService(IDataRepository repository)
        {
            _repository = repository;
        }

        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public bool isBooked { get; set; }

        private ICatalogService Map(ICatalog catalog)
        {
            return new CatalogService(catalog.Id, catalog.RoomNumber, catalog.RoomType, catalog.isBooked);
        }
        public async Task AddCatalogAsync(int id, int roomNumber, string roomType, bool isBooked)
        {
            await this._repository.AddCatalogAsync(id, roomNumber, roomType, isBooked);
        }

        public async Task DeleteCatalogAsync(int id)
        {
            await this._repository.DeleteCatalogAsync(id);
        }

        public async Task<Dictionary<int, ICatalogService>> GetAllCatalogsAsync()
        {
            Dictionary<int, ICatalogService> result = new Dictionary<int, ICatalogService>();

            foreach (ICatalog product in (await this._repository.GetAllCatalogsAsync()).Values)
            {
                result.Add(product.Id, this.Map(product));
            }

            return result;
        }

        public async Task<ICatalogService> GetCatalogAsync(int id)
        {
            return this.Map(await this._repository.GetCatalogAsync(id));
        }

        public async Task<int> GetCatalogsCountAsync()
        {
            return await this._repository.GetCatalogsCountAsync();
        }

        public async Task UpdateCatalogAsync(int id, int roomNumber, string roomType, bool isBooked)
        {
            await this._repository.UpdateCatalogAsync(id, roomNumber, roomType, isBooked);
        }


    }
}
