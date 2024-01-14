using System.Collections.Generic;
using System.Threading.Tasks;
using Manager.Core.Exceptions;
using Manager.Infra.Interfaces;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Manager.Services.Mappers;

namespace Manager.Services.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<UserDTO> CreateAsync(UserDTO userDto)
        {
            var user = await _userRepository.GetByEmailAsync(userDto.Email);

            if (user != null)
                throw new DomainException("User is already registered.");

            var mappedUser = UserMapper.MapToUser((userDto));
            
            var userCreated = await _userRepository.CreateAsync(mappedUser);

            return UserMapper.MapToUserDto(userCreated);
        }

        public async Task<UserDTO> UpdateAsync(UserDTO userDto)
        {
            var user = await _userRepository.GetByIdAsync(userDto.Id);
            
            if( user == null)
                throw new DomainException("User not exists.");
            
            user.Validate();

            var userUpdated = await _userRepository.UpdateAsync(user);

            return UserMapper.MapToUserDto(userUpdated);
        }

        public async Task RemoveAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if(user == null)
                throw new DomainException("User not exists.");
            
            user.Validate();

            await _userRepository.RemoveAsync(id);
        }

        public async Task<UserDTO> GetByIdAsync(long id)
        {
            return UserMapper.MapToUserDto(await _userRepository.GetByIdAsync(id));
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            return UserMapper.MapToUserDtos(await _userRepository.GetAllAsync());
        }

        public async Task<List<UserDTO>> SearchByNameAsync(string name)
        {
            var allUsers = await _userRepository.SearchByNameAsync(name);

            return UserMapper.MapToUserDtos(allUsers);
        }

        public async Task<List<UserDTO>> SearchByEmailAsync(string email)
        {
            var allUsers = await _userRepository.SearchByEmailAsync(email);

            return UserMapper.MapToUserDtos(allUsers);
        }

        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            return UserMapper.MapToUserDto(user);
        }
    }
}