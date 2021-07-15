using Folio3.DotNet.Sbp.Service.School.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Folio3.DotNet.Sbp.Api.Models
{
    public class AuthenticateResponse
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }

    public class AuthenticateRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
