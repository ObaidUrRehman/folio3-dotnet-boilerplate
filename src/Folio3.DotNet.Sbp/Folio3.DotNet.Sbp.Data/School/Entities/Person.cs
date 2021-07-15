using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Folio3.DotNet.Sbp.Data.Common;

namespace Folio3.DotNet.Sbp.Data.School.Entities
{
    public abstract class Person : TrackableEntity, IBaseEntity
    {
        public long ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [Display(Name = "Full Name")] public string FullName => LastName + ", " + FirstMidName;
    }
}