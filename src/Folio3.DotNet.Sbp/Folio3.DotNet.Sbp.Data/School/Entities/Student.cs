﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Folio3.DotNet.Sbp.Data.School.Entities
{
    public class Student : Person
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}