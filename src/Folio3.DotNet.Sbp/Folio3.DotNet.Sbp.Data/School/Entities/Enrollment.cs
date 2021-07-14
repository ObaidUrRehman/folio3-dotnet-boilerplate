using Folio3.DotNet.Sbp.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Folio3.DotNet.Sbp.Data.School.Entities
{
    public enum Grade
    {
        A, B, C, D, F
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
