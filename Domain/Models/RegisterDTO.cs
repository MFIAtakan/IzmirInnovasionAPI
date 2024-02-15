using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Kullanıcı adı alanı zorunludur!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur!")]
        [EmailAddress(ErrorMessage = "Geçersiz bir email adresi girdiniz!")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Parola alanı zorunludur!")]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Required(ErrorMessage = "Rol adı alanı zorunludur!")]
        public string Role { get; set; }
    }
}
