using System;
using Folio3.Sbp.Service.Base;

namespace Folio3.Sbp.Service.School.Dto
{
    public class StudentDto : IDto
    {
        public long ID { get; set; }
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}