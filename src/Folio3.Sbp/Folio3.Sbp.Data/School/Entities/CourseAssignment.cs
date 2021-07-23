using Folio3.Sbp.Data.Common;

namespace Folio3.Sbp.Data.School.Entities
{
    public class CourseAssignment : TrackableEntity, IBaseEntity
    {
        public long InstructorID { get; set; }
        public long CourseID { get; set; }
        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
    }
}