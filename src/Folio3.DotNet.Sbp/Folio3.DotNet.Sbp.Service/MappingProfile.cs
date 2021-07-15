using AutoMapper;
using Folio3.DotNet.Sbp.Data.School.Entities;
using Folio3.DotNet.Sbp.Service.School.Dto;

namespace Folio3.DotNet.Sbp.Service
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