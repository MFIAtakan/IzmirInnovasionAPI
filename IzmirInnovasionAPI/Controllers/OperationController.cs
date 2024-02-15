using Business.IServices;
using Domain.Models;
using IzmırInnovasionCase.Models;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IzmırInnovasionCase.Controllers
{
    [Route("api/operation")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IUserOperationsService _userOperationsService;
        private readonly IIdentityService _identityService;

        public OperationController(IUserOperationsService userOperationsService, IIdentityService identityService)
        {
            _userOperationsService = userOperationsService;
            _identityService = identityService;
        }

        [HttpPost("submitValues")]
        public ApiResponse SubmitValues(UserOperationRequestModel userOperationDTO)
        {
            var resultOfValues = _userOperationsService.CalculateMaxValue(userOperationDTO.Entries.ToList());
            ApiResponse apiResponse = new ApiResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    var userEmail = User.FindFirstValue(ClaimTypes.Email);
                    ApplicationUser user = _identityService.GetLoggedInUser(userEmail).Result;

                    _userOperationsService.ApplyUserEntrances(userOperationDTO.Entries.ToList(), resultOfValues,user);
                    apiResponse.Result = true;
                    apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    apiResponse.IsSuccess = true;
                    return apiResponse;
                }
                catch(Exception ex)
                {
                    apiResponse.ErrorMessages.Add(ex.Message);
                    apiResponse.Result = false;
                    apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    apiResponse.IsSuccess = false;
                    return apiResponse;
                }
            }
            else
            {
                apiResponse.ErrorMessages.Add("Model geçerli değil!");
                apiResponse.Result = false;
                apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                apiResponse.IsSuccess = false;
                return apiResponse;
            }
        }

        [HttpGet("GetUsersInfo")]
        public ApiResponse GetUsersInfo()
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                var userOperations = _userOperationsService.GetAllUserOperations();

                var userOperationInfoList = userOperations.Select(uo => new UserOperationInfoModel
                {
                    Id = uo.Id,
                    CreatedAt = uo.CreatedAt,
                    UpdatedAt = uo.UpdatedAt,
                    UserId = uo.UserId,
                    UserName = uo.UserName,
                    Entries = uo.Entries,
                    Result = uo.Result
                }).ToList();

                apiResponse.Result = userOperationInfoList;
                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                apiResponse.ErrorMessages.Add(ex.Message);
                apiResponse.Result = null;
                apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                apiResponse.IsSuccess = false;
            }

            return apiResponse;
        }
    }
}
