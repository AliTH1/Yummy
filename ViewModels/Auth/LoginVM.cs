using System.ComponentModel.DataAnnotations;

namespace Yummy.ViewModels.Auth
{
    public class LoginVM
    {
        [Required, MaxLength(30), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MinLength(8), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}