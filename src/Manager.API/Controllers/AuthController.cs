using System;
using System.Threading.Tasks;
using Manager.API.Models;
using Manager.API.Models.Request;
using Manager.API.Utils;
using Manager.Services.JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Manager.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
       
        private readonly IConfiguration _configuration;

        private readonly ITokenGenerator _tokenGenerator;
        
        public AuthController(IConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }
        
        [HttpPost]
        [Route("/api/v1/auth/signin")]
        public IActionResult SignIn([FromBody] AuthRequestModel authRequestModel)
        {
            try
            {
                var tokenLogin = _configuration["Jwt:Login"];
                var tokenPassword = _configuration["Jwt:Password"];

                if (authRequestModel.Login == tokenLogin && authRequestModel.Password == tokenPassword)
                {
                    return Ok(new ResultModel
                    {
                        Message = "Usuário autenticado com sucesso!",
                        Success = true,
                        Data = new
                        {
                            Token = _tokenGenerator.GenerateToken(),
                            TokenExpires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"])),
                        }
                    });
                }

                return StatusCode(401, Responses.UnathorizedErrorMessage());
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }       
    }
}