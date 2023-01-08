using SharedKernel;

namespace Core.Entities;

public sealed class User
{
    public string Username { get; }

    public string Password { get; }

    public Role Role { get; }

    public User(string username, string password, Role role)
    {
        Username = NullGuard.ThrowIfNull(username);
        Password = NullGuard.ThrowIfNull(password);
        Role = role;
    }
}