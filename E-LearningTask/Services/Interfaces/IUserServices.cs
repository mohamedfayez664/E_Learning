using DAL.DTO;

namespace E_LearningTask.Services.Interfaces
{
    public interface IUserServices
    {
        List<string> GetUserRighits(int id);
        bool Register(UserRegisterDto model);
        UserDto Login(UserLoginDto model);
        List<UserDto> GetAllUser();
        ResponseDto<UserDto> GetAllUserByFilter(ResponseFilterDto filter);
        bool IsEmailExist(string searchKey);
        bool IsIdExist(int searchKey);
        // bool IsEmailExist<SearchType>(SearchType id);
        bool ActiveUser(int id);
        bool StartUpTheProject();
        bool LinkUserRole(int id, string role);
        bool EditUserInstractor(int id, UserInstructorEditDto model);
        bool AddUserInstractor(UserInstructorAddDto model);
    }
}
