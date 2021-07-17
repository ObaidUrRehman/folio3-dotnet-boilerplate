using System.ComponentModel.DataAnnotations;
using Folio3.Sbp.Data.Common;

namespace Folio3.Sbp.Data.School.Entities
{
    public enum Grade
    {
        A,
        B,
        C,
        D,
        F
    }

    public class Enrollment : TrackableEntity, IBaseEntity
    {
        public long EnrollmentID { get; set; }
        public long CourseID { get; set; }
        public long StudentID { get; set; }

        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}