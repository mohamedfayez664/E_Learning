using AutoMapper;
using DAL.DTO;
using DAL.Entities;

namespace DAL.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ////////User
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserGetDto>().ReverseMap();
            CreateMap<User, UserEditDto>().ReverseMap();

            /////////Course
            ///
            CreateMap<Course, CourseAddDto>().ReverseMap();
            // CreateMap<Course, CourseEditDto>().ReverseMap();
            CreateMap<Course, CourseGetDetailDto>().ReverseMap();

            ////Category
            ///
            CreateMap<Category, CategoryAddDto>().ReverseMap();
            CreateMap<Category, CategoryGetDto>().ReverseMap();

            ///Instructor
            ///
            CreateMap<Instructor, InstructorAddDto>().ReverseMap();
            CreateMap<Instructor, InstructorGetDto>().ReverseMap();
            CreateMap<Instructor, InstructorEditDto>().ReverseMap();


            ////StGroup
            ///
            CreateMap<StGroup, StGroupAddDto>().ReverseMap();
            CreateMap<StGroup, StGroupGetDto>().ReverseMap();

            /////Media
            ///
            CreateMap<Media, MediaAddTypeDto>().ReverseMap();
            CreateMap<Media, MediaEditDataDto>().ReverseMap();

            ////Lesson
            ///
            CreateMap<Lesson, LessonAddDto>().ReverseMap();
            CreateMap<Lesson, LessonGetDataDto>().ReverseMap();
            CreateMap<Lesson, LessonEditDto>().ReverseMap();



            ////PlayList
            ///
            CreateMap<PlayList, PlayListAddDto>().ReverseMap();
            CreateMap<PlayList, PlayListGetDto>().ReverseMap();



        }
    }
}
