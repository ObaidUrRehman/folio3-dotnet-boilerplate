﻿using System.ComponentModel.DataAnnotations;
using Folio3.Sbp.Service.Base;

namespace Folio3.Sbp.Service.School.Dto
{
    public class UserDto : IDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class RegisterModel
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required] [MaxLength(100)] public string Password { get; set; }

        [Required] [MaxLength(100)] public string FirstName { get; set; }

        [Required] [MaxLength(100)] public string LastName { get; set; }
    }
}