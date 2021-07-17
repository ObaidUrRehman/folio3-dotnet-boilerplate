using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Folio3.DotNet.Sbp.Service.Claims
{
    public interface IUserClaims
    {
        public string Id { get; }

        public string Email { get; }

        public string FirstName { get; }

        public string LastName { get; }
    }

    public class UserClaims : IUserClaims
    {
        public UserClaims(IHttpContextAccessor accessor)
        {
            Accessor = accessor;
        }

        private IHttpContextAccessor Accessor { get; }

        private ClaimsIdentity ClaimsIdentity => Accessor?.HttpContext?.User.Identity as ClaimsIdentity;

        public string Id => FirstClaim(UserClaimTypes.Id);

        public string Email => FirstClaim(UserClaimTypes.Email);

        public string FirstName => FirstClaim(UserClaimTypes.FirstName);

        public string LastName => FirstClaim(UserClaimTypes.LastName);

        private string FirstClaim(string claimType)
        {
            return ClaimsIdentity?.FindFirst(claimType)?.Value;
        }
    }
}