using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudentManagementProject.Models;
using StudentManagementProject.Models.Domain;

namespace StudentManagementProject.Helpers
{
    public class AutoMapperProfile : Profile
    {
        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentViewModel>().ReverseMap();
            CreateMap<StudentViewModel, User>().
                ForMember( user => user.NormalizedEmail, opt => opt.MapFrom(stv => stv.Email))
                .ForMember(user =>user.PasswordHash , opt => opt.MapFrom(stv => passwordHasher.HashPassword(null, stv.Password)))
                .ForMember(user => user.Role, opt => opt.MapFrom(svm=>"User"))
                .ForMember(user=>user.Name,opt=>opt.MapFrom(svm=>svm.Firstname))
                .ForMember(user => user.Surname, opt => opt.MapFrom(svm => svm.Lastname))
                .ForMember(user => user.UserName, opt => opt.MapFrom(stv => stv.Email))
                .ForMember(user => user.NormalizedUserName, opt => opt.MapFrom(stv => stv.Email));
        }
    }
}
