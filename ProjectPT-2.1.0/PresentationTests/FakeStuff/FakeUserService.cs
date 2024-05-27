using PresentationTests;
using Service.API;

namespace PresentationTest;

internal class FakeUserService : IUserService
{
    private readonly FakeDataRepository _fakeRepository = new FakeDataRepository();

    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserType { get; set; }
    public FakeUserService()
    {

    }

    public async Task AddUserAsync(int id, string FirstName, string LastName, string UserType)
    {
        await this._fakeRepository.AddUserAsync(id, FirstName, LastName, UserType);
    }

    public async Task<IUserService> GetUserAsync(int id)
    {
        return await this._fakeRepository.GetUserAsync(id);
    }

    public async Task UpdateUserAsync(int id, string FirstName, string LastName, string UserType)
    {
        await this._fakeRepository.UpdateUserAsync(id, FirstName, LastName, UserType);
    }

    public async Task DeleteUserAsync(int id)
    {
        await this._fakeRepository.DeleteUserAsync(id);
    }

    public async Task<Dictionary<int, IUserService>> GetAllUsersAsync()
    {
        Dictionary<int, IUserService> result = new Dictionary<int, IUserService>();

        foreach (IUserService user in (await this._fakeRepository.GetAllUsersAsync()).Values)
        {
            result.Add(user.Id, user);
        }

        return result;
    }

    public async Task<int> GetUsersCountAsync()
    {
        return await this._fakeRepository.GetUsersCountAsync();
    }
}
