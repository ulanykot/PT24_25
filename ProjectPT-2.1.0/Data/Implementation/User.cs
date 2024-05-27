using Data.API;

namespace Data.Implementation;

internal class User : IUser
{
    public User(int id, string firstName, string lastName, string userType)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
    }
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserType { get; set; }

}
