using System.ComponentModel.DataAnnotations;

namespace Yummy.ViewModels.Auth
{
    public class RegisterVM
    {
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [Required, MaxLength(30)]
        public string Surname { get; set; }
        [Required, MaxLength(20)]
        public string UserName { get; set; }
        [Required, MaxLength(30), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MinLength(8), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MinLength(8), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
