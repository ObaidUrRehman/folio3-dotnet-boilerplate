using Folio3.DotNet.Sbp.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Folio3.DotNet.Sbp.Data.School.Entities
{
	public class CourseAssignment : TrackableEntity, IBaseEntity
	{
		public long InstructorID { get; set; }
		public long CourseID { get; set; }
		public Instructor Instructor { get; set; }
		public Course Course { get; set; }
	}
}
