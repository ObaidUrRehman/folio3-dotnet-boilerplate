using AutoMapper;
using Folio3.Sbp.Data.School.Entities;
using Folio3.Sbp.Service.School.Dto;

namespace Folio3.Sbp.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region School

            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

            #endregion
        }
    }
}