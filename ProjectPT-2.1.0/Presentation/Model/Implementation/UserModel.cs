using System;
using Presentation.Model.API;

namespace Presentation.Model.Implementation;

internal class UserModel : IUserModel
{
    public UserModel(int id, string firstName, string lastName, string UserType)
    {
        this.Id = id;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.UserType = UserType;

    }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserType { get; set; }
}
