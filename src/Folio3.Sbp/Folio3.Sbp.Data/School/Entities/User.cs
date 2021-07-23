using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Folio3.Sbp.Data.School.Entities
{
    public class User : IdentityUser
    {
        [StringLength(200)] [PersonalData] public string FirstName { get; set; }

        [StringLength(200)] [PersonalData] public string LastName { get; set; }

        [Display(Name = "Name")] public string FullName => $"{FirstName} {LastName}";
    }
}