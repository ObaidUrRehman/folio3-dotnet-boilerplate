using System.ComponentModel.DataAnnotations;
using Folio3.Sbp.Service.School.Dto;

namespace Folio3.Sbp.Api.Models
{
    public class AuthenticateResponse
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }

    public class AuthenticateRequest
    {
        [Required] public string UserName { get; set; }

        [Required] public string Password { get; set; }
    }
}