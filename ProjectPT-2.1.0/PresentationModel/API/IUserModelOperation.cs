﻿using Service.API;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PresentationModel.Implementation;

namespace PresentationModel;

public interface IUserModelOperation
{
    static IUserModelOperation CreateModelOperation(IUserService? userCrud = null)
    {
        return new UserModelOperation(userCrud);
    }

    Task AddAsync(int id, string firstName, string lastName, string userType);

    Task<IUserModel> GetAsync(int id);

    Task UpdateAsync(int id, string firstName, string lastName, string userType);

    Task DeleteAsync(int id);

    Task<Dictionary<int, IUserModel>> GetAllAsync();

    Task<int> GetCountAsync();
}
