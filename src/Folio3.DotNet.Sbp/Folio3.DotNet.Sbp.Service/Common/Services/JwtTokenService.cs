using Folio3.DotNet.Sbp.Common.Settings;
using Folio3.DotNet.Sbp.Data.School.Entities;
using Folio3.DotNet.Sbp.Service.Claims;
using Folio3.DotNet.Sbp.Service.School.Dto;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Folio3.DotNet.Sbp.Service.Common.Services
{
    public class JwtTokenService
    {
        public JwtTokenSettings JwtTokenSettings { get; }

        public JwtTokenService(IOptions<JwtTokenSettings> jwtTokenSettings)
        {
            JwtTokenSettings = jwtTokenSettings.Value;
        }

        public string CreateToken(UserDto user)
        {
            var claims = UserClaimTypes.GetClaims(user);

            return new JwtSecurityTokenHandler()
                .WriteToken(new JwtSecurityToken(
                    issuer: JwtTokenSettings.Issuer,
                    audience: JwtTokenSettings.Audience,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddDays(7),
                    claims: claims,
                    signingCredentials: new SigningCredentials(
                        key: new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtTokenSettings.Secret)),
                        algorithm: SecurityAlgorithms.HmacSha256Signature)));
        }
    }
}
