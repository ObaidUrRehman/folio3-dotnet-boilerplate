using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Folio3.Sbp.Data.School;
using Folio3.Sbp.Data.School.Entities;
using Folio3.Sbp.Service.Common;
using Folio3.Sbp.Service.Common.Services;
using Folio3.Sbp.Service.School.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Folio3.Sbp.Service.School.Services
{
    public class UserService : BaseService <User, UserDto>
    {
        public UserService(
            SchoolDbContext context,
            UserManager<User> userManager,
            ILogger<UserService> logger,
            IMapper mapper)
            : base(context, logger, mapper)
        {
            UserManager = userManager;
        }

        public UserManager<User> UserManager { get; }

        public async Task<ServiceResult<UserDto>> CreateUserAsync(RegisterModel registerModel)
        {
            return await CreateUserAsync(new User
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName
            }, registerModel.Password);
        }

        public async Task<ServiceResult<UserDto>> CreateUserAsync(User user, string password)
        {
            var result = await UserManager.CreateAsync(user, password);
            return result.Succeeded
                ? Success(Mapper.Map<UserDto>(user))
                : Failure(result.Errors.Select(r => r.Description).ToList());
        }

        public async Task<ServiceResult<UserDto>> VerifyUserAsync(string userName, string password)
        {
            var user = await UserManager.FindByNameAsync(userName);

            if (user == null)
                return Failure("User does not exist.");

            if (!await UserManager.CheckPasswordAsync(user, password))
                return Failure("Password did not match.");

            return Success(Mapper.Map<UserDto>(user));
        }

        public IQueryable<User> AllUsers()
        {
            return UserManager.Users;
        }
    }
}