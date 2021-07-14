using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Folio3.DotNet.Sbp.Service
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			#region School
			CreateMap<Data.School.Entities.Student, School.Dto.StudentDto>().ReverseMap();
			CreateMap<Data.School.Entities.User, School.Dto.UserDto>().ReverseMap();
			#endregion
		}
	}
}
