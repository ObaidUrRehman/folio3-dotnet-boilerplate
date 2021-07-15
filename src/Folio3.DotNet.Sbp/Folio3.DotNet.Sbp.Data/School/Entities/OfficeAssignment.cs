using System.ComponentModel.DataAnnotations;
using Folio3.DotNet.Sbp.Data.Common;

namespace Folio3.DotNet.Sbp.Data.School.Entities
{
    public class OfficeAssignment : TrackableEntity, IBaseEntity
    {
        [Key] public long InstructorID { get; set; }

        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public Instructor Instructor { get; set; }
    }
}