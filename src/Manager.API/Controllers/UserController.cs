using System;
using System.Threading.Tasks;
using Manager.API.Models;
using Manager.API.Models.Request;
using Manager.API.Utils;
using Manager.Core.Exceptions;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        [Authorize]
        [Route("/api/v1/users/create")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestModel userRequestModel)
        {
            try
            {
                var userDto = new UserDTO
                {
                    Name = userRequestModel.Name,
                    Password = userRequestModel.Password,
                    Email = userRequestModel.Email
                };
                var userCreated = await _userService.CreateAsync(userDto);
                return Ok(new ResultModel
                {
                    Message = "Usuário criado com sucesso!",
                    Success = true,
                    Data = userCreated
                });
            }
            catch (DomainException error)
            {
                return BadRequest(Responses.DomainErrorMessage(error.Message, error.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
        
        [HttpPut]
        [Authorize]
        [Route("/api/v1/users/update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequestModel userUpdateRequestModel)
        {
            try
            {
                var userDto = new UserDTO
                {
                    Id = userUpdateRequestModel.Id,
                    Name = userUpdateRequestModel.Name,
                    Password = userUpdateRequestModel.Password,
                    Email = userUpdateRequestModel.Email
                };
                var userCreated = await _userService.UpdateAsync(userDto);
                return Ok(new ResultModel
                {
                    Message = "Usuário atualizado com sucesso!",
                    Success = true,
                    Data = userCreated
                });
            }
            catch (DomainException error)
            {
                return BadRequest(Responses.DomainErrorMessage(error.Message, error.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
        
        [HttpDelete]
        [Authorize]
        [Route("/api/v1/users/remove/{id}")]
        public async Task<IActionResult> RemoveUser(long id)
        {
            try
            {
                await _userService.RemoveAsync(id);
                return Ok(new ResultModel
                {
                    Message = "Usuário removido com sucesso!",
                    Success = true,
                    Data = id
                });
            }
            catch (DomainException error)
            {
                return BadRequest(Responses.DomainErrorMessage(error.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
        
        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/{id}")]
        public async Task<IActionResult> GetUser(long id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);

                if (user == null)
                    return NotFound(new ResultModel
                    {
                        Message = "Nenhum usuário foi encontrado.",
                        Success = false,
                        Data = null
                    });
                
                
                return Ok(new ResultModel
                {
                    Message = "Usuário encontrado com sucesso!",
                    Success = true,
                    Data = user
                });
            }
            catch (DomainException error)
            {
                return BadRequest(Responses.DomainErrorMessage(error.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
        
        [HttpGet]
        [Authorize]
        [Route("/api/v1/users")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var user = await _userService.GetAllAsync();

                if (user == null)
                    return NotFound(new ResultModel
                    {
                        Message = "Nenhum usuário foi encontrado.",
                        Success = false,
                        Data = null
                    });
                
                
                return Ok(new ResultModel
                {
                    Message = "Usuários encontrados com sucesso!",
                    Success = true,
                    Data = user
                });
            }
            catch (DomainException error)
            {
                return BadRequest(Responses.DomainErrorMessage(error.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
    }
}