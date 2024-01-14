using System.Collections.Generic;
using System.Linq;
using Manager.Domain.Entities;
using Manager.Services.DTO;

namespace Manager.Services.Mappers
{
    public static class UserMapper
    {
        
        public static UserDTO MapToUserDto(User  user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
        }
        public static User MapToUser(UserDTO  userDto)
        {
            return new User(userDto.Name, userDto.Email, userDto.Password);
        }
        
        
        public static List<UserDTO> MapToUserDtos(List<User> users)
        {
            return users.Select(user => new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            }).ToList();

        }
        
    }
}