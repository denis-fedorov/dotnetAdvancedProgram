using Core.Entities;
using Infrastructure.Dtos;
using SharedKernel;

namespace Infrastructure.Extensions;

public static class MappingExtension
{
    public static UserDto ToDto(this User user)
    {
        NullGuard.ThrowIfNull(user);
        
        return new UserDto
        {
            Username = user.Username,
            Password = user.Password,
            Role = (byte)user.Role
        };
    }

    public static User ToEntity(this UserDto userDto)
    {
        NullGuard.ThrowIfNull(userDto);
        
        return new User(userDto.Username, userDto.Password, (Role)userDto.Role);
    }
}