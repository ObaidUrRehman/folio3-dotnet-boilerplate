using System.Linq;
using System.Threading.Tasks;
using Folio3.Sbp.Api.Attributes;
using Folio3.Sbp.Api.Models;
using Folio3.Sbp.Service.Common.Dto;
using Folio3.Sbp.Service.Common.Services;
using Folio3.Sbp.Service.School.Dto;
using Folio3.Sbp.Service.School.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Folio3.Sbp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        public UserController(
            ILogger<UserController> logger,
            UserService userService,
            JwtTokenService jwtTokenService
        ) : base(logger)
        {
            UserService = userService;
            JwtTokenService = jwtTokenService;
        }

        public UserService UserService { get; }
        public JwtTokenService JwtTokenService { get; }

        [HttpPost("register")]
        [ValidateModel]
        public async Task<ResponseDto<AuthenticateResponse>> Register([FromBody] RegisterModel registerModel)
        {
            if (UserService.AllUsers().Any(u => u.Email == registerModel.Email))
                return BadRequest<AuthenticateResponse>($"A user with {registerModel.Email} already exists.");

            var result = await UserService.CreateUserAsync(registerModel);
            if (!result.Success)
                return BadRequest<AuthenticateResponse>(string.Empty, result.Errors);

            return Success(new AuthenticateResponse
            {
                User = result.Data
            });
        }

        [HttpPost("authenticate")]
        public async Task<ResponseDto<AuthenticateResponse>> Authenticate(AuthenticateRequest requestModel)
        {
            var response = await UserService.VerifyUserAsync(requestModel.UserName, requestModel.Password);

            if (!response.Success)
                return Unauthorized<AuthenticateResponse>("Username or password is incorrect");

            return Success(new AuthenticateResponse
            {
                User = response.Data,
                Token = JwtTokenService.CreateToken(response.Data)
            });
        }
    }
}