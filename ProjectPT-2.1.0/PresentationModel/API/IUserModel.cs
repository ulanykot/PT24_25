using System;

namespace Presentation.Model.API;

public interface IUserModel
{
    int Id { get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }

    string UserType { get; set; }

}
