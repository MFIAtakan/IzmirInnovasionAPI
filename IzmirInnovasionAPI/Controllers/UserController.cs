using Business.IServices;
using Data.Data;
using Data.Database;
using Domain.Models;
using IzmırInnovasionCase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IzmırInnovasionCase.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("login")]
        public ApiResponse Login([FromBody] LoginDTO model)
        {
            BusinessResponse response = null;
            ApiResponse apiResponse = new ApiResponse();

            if (ModelState.IsValid)
            {
                response = _identityService.Login(model).Result;
            }

            if (response != null && response.IsSuccess)
            {

                apiResponse.IsSuccess = true;
                apiResponse.Result = response.Result;
                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                return apiResponse;
            }
            else
            {
                apiResponse.IsSuccess = false;
                apiResponse.Result = response;
                apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return apiResponse;
            }
        }



        [HttpPost("register")]
        public ApiResponse Register([FromBody] RegisterDTO model)
        {
            BusinessResponse response = null;
            ApiResponse apiResponse = new ApiResponse();

            if (ModelState.IsValid)
            {
                response = _identityService.Register(model).Result;
            }
            if (response != null && response.IsSuccess)
            {
                apiResponse.IsSuccess = true;
                apiResponse.Result = response;
                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                return apiResponse;
            }
            else
            {
                apiResponse.IsSuccess = false;
                apiResponse.Result = response;
                apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return apiResponse;
            }
        }

        [HttpGet("logout")]
        public ApiResponse Logout()
        {
            ApiResponse response = new ApiResponse();
            _identityService.Logout();
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.Accepted;
            response.Result = true;

            return response;
        }
    }
}
