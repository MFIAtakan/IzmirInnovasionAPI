using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Kullanıcı adı alanı zorunludur!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parola alanı zorunludur!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
