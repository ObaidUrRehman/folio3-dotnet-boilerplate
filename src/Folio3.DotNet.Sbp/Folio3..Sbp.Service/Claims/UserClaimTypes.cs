using System.Collections.Generic;
using System.Security.Claims;
using Folio3.Sbp.Service.School.Dto;

namespace Folio3.Sbp.Service.Claims
{
    public class UserClaimTypes
    {
        public const string Id = ClaimTypes.NameIdentifier;
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string Email = ClaimTypes.Email;

        public static IList<Claim> GetClaims(UserDto user)
        {
            return new List<Claim>
            {
                new Claim(Id, user.Id),
                new Claim(FirstName, user.FirstName),
                new Claim(LastName, user.LastName),
                new Claim(Email, user.Email)
            };
        }
    }
}