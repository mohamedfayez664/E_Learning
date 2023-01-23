using DAL.Data;
using DAL.Entities;
using E_LearningTask.Services.Interfaces;

namespace E_LearningTask.Services
{
    public class UserCourseServices : IUserCourseServices
    {
        private readonly ApplicationDBContext _context;
        public UserCourseServices(ApplicationDBContext context)
        {
            _context = context;
        }

        public bool LinkUserToCourse(int user_id, int course_id)
        {
            /////
            var _usercourse = _context.UserCourses.Any(uc => (uc.CourseId == course_id) && (uc.UserId == user_id));
            if (_usercourse == true) return false;
            if (_context.Users.Find(user_id) != null && _context.Courses.Find(course_id) != null)
            {
                var _usercourseLink = new UserCourse
                {
                    CourseId = course_id,
                    UserId = user_id,
                };

                _context.UserCourses.Add(_usercourseLink);
                _context.SaveChanges();
            }

            else
            {
                return false;
            }
            return true;
        }



    }
}
