using Business.IServices;
using Data.Database;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Business.Services
{
    public class IdentityServiceImpl : IIdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext context;

        public IdentityServiceImpl(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        public async Task<BusinessResponse> Login(LoginDTO loginDTO)
        {
            BusinessResponse response = new BusinessResponse();
            var result = await signInManager.PasswordSignInAsync(loginDTO.UserName,
                          loginDTO.Password, loginDTO.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(loginDTO.UserName);
                var userRoles = await userManager.GetRolesAsync(user);

                response.IsSuccess = true;
                response.Result = new { UserName = loginDTO.UserName, Roles = userRoles };
                return response;
            }


            response.IsSuccess = false;
            response.ErrorMessages.Add("Kullanıcı girişi yapılamadı, bilgilerinizi kontrol ediniz!");
            return response;
        }

        public async void Logout()
        {
            await signInManager.SignOutAsync();

        }

        public async Task<BusinessResponse> Register(RegisterDTO registerDTO)
        {
            ApplicationUser user = context.Users.FirstOrDefault(u=> u.UserName == registerDTO.UserName);
            BusinessResponse response = new BusinessResponse();
            if (user != null)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add("Kullanıcı zaten bulunuyor!");
                return response;
            }
            ApplicationUser newUser = new ApplicationUser()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,

            };
            try
            {
                var result = await userManager.CreateAsync(newUser, registerDTO.Password);
                {
                    if (!roleManager.RoleExistsAsync(Roles.Admin).GetAwaiter().GetResult())
                    {
                        //create roles in database
                        await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                        await roleManager.CreateAsync(new IdentityRole(Roles.User));
                    }
                    if (registerDTO.Role == Roles.Admin)
                    {
                        await userManager.AddToRoleAsync(newUser, Roles.Admin);
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(newUser, Roles.User);
                    }

                    response.IsSuccess = true;
                    response.Result = newUser;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                return response;
            }
        }

        public async Task<ApplicationUser> GetLoggedInUser(string email)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            return user;
        }
    }
}
