﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Presentation.Model.Implementation;
using Service.API;

namespace Presentation.Model.API;

public interface ICatalogModelOperation
{
    static ICatalogModelOperation CreateModelOperation(ICatalogService? catalogCrud = null)
    {
        return new CatalogModelOperation(catalogCrud ?? ICatalogService.CreateCatalogService());
    }

    Task AddAsync(int id, int roomNumber, string roomType, bool isBooked);

    Task<ICatalogModel> GetAsync(int id);

    Task UpdateAsync(int id, int roomNumber, string roomType, bool isBooked);

    Task DeleteAsync(int id);

    Task<Dictionary<int, ICatalogModel>> GetAllAsync();

    Task<int> GetCountAsync();
}
