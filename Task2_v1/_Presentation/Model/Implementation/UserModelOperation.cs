using Presentation.Model.API;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ServiceLayer.API;
using ServiceLayer.Implementation;

namespace Presentation.Model.Implementation;

internal class UserModelOperation : IUserModelOperation
{
    private IUserService _userCRUD;

    public UserModelOperation(IUserService? userCrud)
    {
        this._userCRUD = userCrud ?? ServiceFactory.CreateUserService();
    }

    private IUserModel Map(IUserService user)
    {
        return new UserModel(user.Id, user.FirstName, user.LastName, user.UserType);
    }

    public async Task AddAsync(int id, string firstName, string lastName, string userType)
    {
        await this._userCRUD.AddUserAsync(id, firstName, lastName, userType);
    }

    public async Task<IUserModel> GetAsync(int id)
    {
        return this.Map(await this._userCRUD.GetUserAsync(id));
    }

    public async Task UpdateAsync(int id, string firstName, string lastName, string userType)
    {
        await this._userCRUD.UpdateUserAsync(id, firstName, lastName, userType);
    }

    public async Task DeleteAsync(int id)
    {
        await this._userCRUD.DeleteUserAsync(id);
    }

    public async Task<Dictionary<int, IUserModel>> GetAllAsync()
    {
        Dictionary<int, IUserModel> result = new Dictionary<int, IUserModel>();

        foreach (IUserService user in (await this._userCRUD.GetAllUsersAsync()).Values)
        {
            result.Add(user.Id, this.Map(user));
        }

        return result;
    }

    public async Task<int> GetCountAsync()
    {
        return await this._userCRUD.GetUsersCountAsync();
    }
}
