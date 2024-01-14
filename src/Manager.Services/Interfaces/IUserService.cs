using System.Collections.Generic;
using System.Threading.Tasks;
using Manager.Services.DTO;

namespace Manager.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateAsync(UserDTO userDto);
        
        Task<UserDTO> UpdateAsync(UserDTO userDto);
        
        Task RemoveAsync(long id);
        
        Task<UserDTO> GetByIdAsync(long id);
        
        Task<List<UserDTO>> GetAllAsync();
        
        Task<List<UserDTO>> SearchByNameAsync(string name);
        
        Task<List<UserDTO>> SearchByEmailAsync(string email);
        
        Task<UserDTO> GetByEmailAsync(string email);
    }
}