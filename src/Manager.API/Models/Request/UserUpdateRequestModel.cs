using System.ComponentModel.DataAnnotations;

namespace Manager.API.Models.Request
{
    public class UserUpdateRequestModel
    {
        
        [Required(ErrorMessage = "O id não pode ser vazio.")]
        public long Id { get; set; }
        
        [Required(ErrorMessage = "O nome não pode ser vazio.")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
        [MaxLength(80, ErrorMessage = "O nome deve ter no máximo 80 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O email não pode ser vazio.")]
        [MinLength(10, ErrorMessage = "O email deve ter no mínimo 10 caracteres.")]
        [MaxLength(100, ErrorMessage = "O email deve ter no máximo 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", 
            ErrorMessage = "O email não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha não pode ser vazia.")]
        [MinLength(10, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        [MaxLength(100, ErrorMessage = "A senha deve ter no máximo 80 caracteres.")]
        public string Password { get; set; }
    }
}