using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

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
        private IHttpContextAccessor Accessor { get; }

        public UserClaims(IHttpContextAccessor accessor)
        {
            Accessor = accessor;
        }

        public string Id => FirstClaim(UserClaimTypes.Id);

        public string Email => FirstClaim(UserClaimTypes.Email);

        public string FirstName => FirstClaim(UserClaimTypes.FirstName);

        public string LastName => FirstClaim(UserClaimTypes.LastName);

        private ClaimsIdentity ClaimsIdentity => Accessor?.HttpContext?.User.Identity as ClaimsIdentity;

        private string FirstClaim(string claimType) => ClaimsIdentity?.FindFirst(claimType)?.Value;
    }
}
