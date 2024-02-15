using Data.Database;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface IIdentityService
    {
        public Task<BusinessResponse> Login(LoginDTO loginDTO);
        public void Logout();
        public Task<BusinessResponse> Register(RegisterDTO registerDTO);

        public Task<ApplicationUser> GetLoggedInUser(string email);

    }
}
