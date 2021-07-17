using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Folio3.Sbp.Common.Settings;
using Folio3.Sbp.Service.Claims;
using Folio3.Sbp.Service.School.Dto;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Folio3.Sbp.Service.Common.Services
{
    public class JwtTokenService
    {
        public JwtTokenService(IOptions<JwtTokenSettings> jwtTokenSettings)
        {
            JwtTokenSettings = jwtTokenSettings.Value;
        }

        public JwtTokenSettings JwtTokenSettings { get; }

        public string CreateToken(UserDto user)
        {
            var claims = UserClaimTypes.GetClaims(user);

            return new JwtSecurityTokenHandler()
                .WriteToken(new JwtSecurityToken(
                    JwtTokenSettings.Issuer,
                    JwtTokenSettings.Audience,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddDays(7),
                    claims: claims,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtTokenSettings.Secret)),
                        SecurityAlgorithms.HmacSha256Signature)));
        }
    }
}