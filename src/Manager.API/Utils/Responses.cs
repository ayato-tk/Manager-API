using System.Collections.Generic;
using Manager.API.Models;

namespace Manager.API.Utils
{
    public static class Responses
    {

        public static ResultModel ApplicationErrorMessage()
        {
            return new ResultModel
            {
                Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente.",
                Success = false,
                Data = null
            };
        }
        
        public static ResultModel DomainErrorMessage(string message)
        {
            return new ResultModel
            {
                Message = message,
                Success = false,
                Data = null
            };
        }
        
        public static ResultModel DomainErrorMessage(string message, IReadOnlyCollection<string> errors)
        {
            return new ResultModel
            {
                Message = message,
                Success = false,
                Data = errors
            };
        }
        
        public static ResultModel UnathorizedErrorMessage()
        {
            return new ResultModel
            {
                Message = "A combinação de login e senha está incorreta!",
                Success = false,
                Data = null
            };
        }
        
    }
}