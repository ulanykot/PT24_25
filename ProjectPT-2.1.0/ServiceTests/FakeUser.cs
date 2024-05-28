using Data.API;

namespace ServiceTests;

internal class FakeUser : IUser
{
    public FakeUser(int id, string firstName, string lastName, string userType)
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
