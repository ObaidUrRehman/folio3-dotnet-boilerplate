using Folio3.DotNet.Sbp.Data.School.Entities;
using Folio3.DotNet.Sbp.Service.School.Dto;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Folio3.DotNet.Sbp.Service.Claims
{
    public class UserClaimTypes
    {
        public const string Id = ClaimTypes.NameIdentifier;
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string Email = ClaimTypes.Email;

        public static IList<Claim> GetClaims(UserDto user)
            => new List<Claim>
            {
                new Claim(Id, user.Id),
                new Claim(FirstName, user.FirstName),
                new Claim(LastName, user.LastName),
                new Claim(Email, user.Email),
            };
    }
}
