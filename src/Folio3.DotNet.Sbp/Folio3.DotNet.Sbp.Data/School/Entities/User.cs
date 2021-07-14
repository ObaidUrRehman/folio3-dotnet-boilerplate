using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Folio3.DotNet.Sbp.Data.School.Entities
{
	public class User : IdentityUser
	{
        [StringLength(200)]
        [PersonalData]
        public string FirstName { get; set; }

        [StringLength(200)]
        [PersonalData]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
