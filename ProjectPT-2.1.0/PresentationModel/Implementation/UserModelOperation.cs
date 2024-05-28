using Presentation.Model.API;
using Service.API;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PresentationModel.Implementation;

namespace PresentationModel.Implementation;

internal class UserModelOperation : IUserModelOperation
{
    private IUserService _userCRUD;

    public UserModelOperation(IUserService? userCrud)
    {
        _userCRUD = userCrud ?? IUserService.CreateUserService();
    }

    private IUserModel Map(IUserService user)
    {
        return new UserModel(user.Id, user.FirstName, user.LastName, user.UserType);
    }

    public async Task AddAsync(int id, string firstName, string lastName, string userType)
    {
        await _userCRUD.AddUserAsync(id, firstName, lastName, userType);
    }

    public async Task<IUserModel> GetAsync(int id)
    {
        return this.Map(await _userCRUD.GetUserAsync(id));
    }

    public async Task UpdateAsync(int id, string firstName, string lastName, string userType)
    {
        await _userCRUD.UpdateUserAsync(id, firstName, lastName, userType);
    }

    public async Task DeleteAsync(int id)
    {
        await _userCRUD.DeleteUserAsync(id);
    }

    public async Task<Dictionary<int, IUserModel>> GetAllAsync()
    {
        Dictionary<int, IUserModel> result = new Dictionary<int, IUserModel>();

        foreach (IUserService user in (await _userCRUD.GetAllUsersAsync()).Values)
        {
            result.Add(user.Id, Map(user));
        }

        return result;
    }

    public async Task<int> GetCountAsync()
    {
        return await _userCRUD.GetUsersCountAsync();
    }
}
