using System.ComponentModel.DataAnnotations;

namespace Manager.API.Models.Request
{
    public class AuthRequestModel
    {
        [Required(ErrorMessage = "O nome não pode ser vazio.")]
        public string Login { get; set; }
        
        
        [Required(ErrorMessage = "A senha não pode ser vazia")]
        public string Password { get; set; }
    }
}