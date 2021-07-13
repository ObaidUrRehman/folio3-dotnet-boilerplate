using Folio3.DotNet.Sbp.Data.Base.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Folio3.DotNet.Sbp.Data.School.Entities
{
	public class CourseAssignment : BaseEntity
	{
		public int InstructorID { get; set; }
		public int CourseID { get; set; }
		public Instructor Instructor { get; set; }
		public Course Course { get; set; }
	}
}
