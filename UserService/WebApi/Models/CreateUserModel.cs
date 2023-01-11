using Core.Entities;

namespace WebApi.Models;

public class CreateUserModel
{
    public string Username { get; set; }

    public string Password { get; set; }

    public Role Role { get; set; }

    public User ToUser()
    {
        return new User(Username, Password, Role);
    }
}