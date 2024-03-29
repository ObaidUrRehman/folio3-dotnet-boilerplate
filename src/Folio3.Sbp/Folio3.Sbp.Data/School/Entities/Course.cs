﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Folio3.Sbp.Data.Common;

namespace Folio3.Sbp.Data.School.Entities
{
    public class Course : TrackableEntity, IBaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public long CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)] public string Title { get; set; }

        [Range(0, 5)] public int Credits { get; set; }

        public long DepartmentID { get; set; }

        public Department Department { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<CourseAssignment> CourseAssignments { get; set; }
    }
}