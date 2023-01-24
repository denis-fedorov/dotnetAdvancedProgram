using System;
using Core.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace CoreTests;

public class UserTests
{
    [Fact]
    public void CreateUser_PassesEmptyPassword_ThrowsException()
    {
        const string password = null!;
        const string username = TestData.Username;
        const Role role = TestData.Role;

        var act = () => CreateUser(username, password!, role);

        act.Should().ThrowExactly<ArgumentNullException>();
    }
    
    [Fact]
    public void CreateUser_PassesEmptyUsername_ThrowsException()
    {
        const string password = TestData.Password;
        const string username = null!;
        const Role role = TestData.Role;

        var act = () => CreateUser(username!, password, role);

        act.Should().ThrowExactly<ArgumentNullException>();
    }
    
    [Fact]
    public void CreateUser_PassesNonNullParameters_CreatesUser()
    {
        const string password = TestData.Password;
        const string username = TestData.Username;
        const Role role = TestData.Role;

        var user = CreateUser(username, password, role);

        using (new AssertionScope())
        {
            user.Should().NotBeNull();
            user.Username.Should().Be(username);
            user.Password.Should().Be(password);
            user.Role.Should().Be(role);
        }
    }

    [Fact]
    public void Test()
    {
        false.Should().BeTrue();
    }

    private static User CreateUser(string username, string password, Role role)
    {
        return new User(username, password, role);
    }
    
    private static class TestData
    {
        public const string Username = "username";

        public const string Password = "password";

        public const Role Role = Core.Entities.Role.Buyer;
    }
}