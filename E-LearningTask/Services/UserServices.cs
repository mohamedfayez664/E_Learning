using AutoMapper;
using DAL.Data;
using DAL.DTO;
using DAL.Entities;
using DAL.Entities.NotMapped;
using E_LearningTask.Services.Helper;
using E_LearningTask.Services.Interfaces;
using Ganss.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_LearningTask.Services
{
    public class UserServices : IUserServices
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly IExtension _extension;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRoleServices _roleServices;
        public UserServices(ApplicationDBContext context, IMapper mapper, IExtension extension, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IRoleServices roleServices)
        {
            _context = context;
            _mapper = mapper;
            _extension = extension;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _roleServices = roleServices;
        }


        public List<UserDto> GetAllUser()
        {
            var res = _context.Users.ToList();
            UserDto tempout = null;
            var Dtoout = new List<UserDto>();
            foreach (var item in res)
            {
                tempout = _mapper.Map<UserDto>(item);
                Dtoout.Add(tempout);
            }
            //var res2=  _mapper.Map<UserDto>(res);
            return Dtoout;
        }

        public ResponseDto<UserDto> GetAllUserByFilter(ResponseFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public bool IsEmailExist(string email)
        {
            return _context.Users.Any(u => u.Email == email || u.UserName == email);
        }
        public bool IsIdExist(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public UserDto Login(UserLoginDto model)
        {
            //check Db
          
            var user = _context.Users.FirstOrDefault(u => (u.UserName == model.Email || u.Email == model.Email) && u.Password == model.Password);
            if (user == null)
            {
                return null;
            }

            //create token
            var _mySecretkey = Encoding.ASCII.GetBytes(_configuration["SecretKey"]);

            // var _rights = GetUserRighits(user.Id);
            // foreach (var role in _rights)   Claim.add (new Claim(ClaimTypes.Role, role));

            JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
           
            var _securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.Now.AddMinutes(30),
             
                //Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                //{
                //    new Claim(ClaimTypes.NameIdentifier, user.Email),
                //    new Claim(ClaimTypes.Email, user.Email),
                //    new Claim("Id", user.Id.ToString()), ///////Important One
                //    new Claim("password", user.Password),
                //    new Claim(ClaimTypes.Role, "GetUserRighits"),   ///>>>>>List of RolesIds >>> rolename
                //   }),

                Subject = getClaimsIdentity(user.Id, user.UserName),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_mySecretkey)
                                                              , SecurityAlgorithms.HmacSha256Signature
                                                           )
            };

            var _securitytoken = _jwtSecurityTokenHandler.CreateToken(_securityTokenDescriptor);
            var _token = _jwtSecurityTokenHandler.WriteToken(_securitytoken);

            var res = _mapper.Map<UserDto>(user);
            res.Token = _token;
            return res;
        }


        public ClaimsIdentity getClaimsIdentity(int uid, string userName)
        {
            var _rights = GetUserRighits(uid);
            return new ClaimsIdentity( getClaims()  );
            ///Function
            Claim[] getClaims()
            {
                var _claims = new List<Claim>();
                _claims.Add(new Claim(ClaimTypes.Name, userName));
                foreach (var item in _rights)
                {
                    _claims.Add(new Claim(ClaimTypes.Role, item));
                }
                return _claims.ToArray();
            }
        }


        public bool Register(UserRegisterDto model)
        {
            var user = _mapper.Map<User>(model);

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                ////////////////////////////////
                /////Email
                /////
                string _UserMail = user.Email;
                string supject = "E_Learning Activate MyAccount";
                string message = $"Dear {user.Name}, \n\n Please click here To Activate your account\n <a href=\"https://localhost:7172/api/Users/ActiveUser?id={user.Id}\"> Active </a>";
                var emailsend = _extension.SendEmail(_UserMail, supject, message);

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public bool ActiveUser(int id)
        {
            //active user
            var user = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.Id == id);

            if (user != null) user.IsActive = true;

            else return false;

            _context.Update(user);
            _context.SaveChanges();
            return true;
        }

        public List<string> GetUserRighits(int id)
        {
            ////////////////////
            var res = _context.UserRoles.Where(_ur => _ur.UserId == id)
               .Join(_context.RoleRights, ur => ur.RoleId, rr => rr.RoleId, (ur, rr)
                 => new { _Roleright = rr.RightId })
               .Join(_context.Rights, n_rr => n_rr._Roleright, rt => rt.Id, (n_rr, rt)
                 => new //RightNameDto  
                 {
                     Name = rt.Name,
                 }).Distinct().Select(a => a.Name).ToList(); ;

            return res;
        }


        public bool StartUpTheProject()
        {
            ///1.
            var _addRightAndRole = Start_addRoleRights();

            ////2.
            var _Righits = _context.Rights.Select(r => r.Name).ToList();
            var _addRoleRight = _roleServices.LinkRoleRights("Admin", _Righits);

            ////3
            var _user = new User()
            {
                Name = "Elearning User",
                UserName = "Elearning",
                Email = "Elearning@Elearning.com",
                Password = "123",
                Adress = "Cairo",
                Phone = "01010101212",
                IsActive = true,
            };
            if (!IsEmailExist(_user.Email))
            {
                _context.Users.Add(_user);
                _context.SaveChanges();
            }

            ////4
            ///
            var _userId = _context.Users.FirstOrDefault(r => r.Email == _user.Email).Id;
            var _addUserRole = LinkUserRole(_userId, "Admin");

            return true;
        }



        public bool LinkUserRole(int id, string role)
        {
            //check
            var _roleId = _context.Roles.FirstOrDefault(r => r.Name == role).Id;
            var _userRole = new UserRole()
            {
                UserId = id,
                RoleId = _roleId,
            };

            try
            {
                if ((_context.UserRoles.FirstOrDefault(r => r.UserId == id || r.RoleId == _roleId)) == null)
                {
                    _context.UserRoles.Add(_userRole);
                }
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Start_addRoleRights()
        {
            //Update Database Table ("RigitExcel") and ("Role")
            var fileRightdistnation = _webHostEnvironment.ContentRootPath + @"\Files\RightExcel.xlsx";
            var _rightExcelData = new ExcelMapper(fileRightdistnation).Fetch<RightExcel>();
            var _rightsDb = _context.Rights.Select(r => r.Name).ToList();

            var fileRoledistnation = _webHostEnvironment.ContentRootPath + @"\Files\RoleExcel.xlsx";
            var _roleExcelData = new ExcelMapper(fileRoledistnation).Fetch<RoleExcel>();
            var _rolesDb = _context.Roles.Select(r => r.Name).ToList();

            try
            {
                foreach (var _right in _rightExcelData)
                {
                    if (!_rightsDb.Contains(_right.Name))
                    {
                        var _newRigt = new Right();
                        _newRigt.Name = _right.Name;
                        _newRigt.Description = _right.Description;

                        _context.Rights.Add(_newRigt);
                    }
                }

                foreach (var _role in _roleExcelData)
                {
                    if (!_rolesDb.Contains(_role.Name))
                    {
                        var _newRole = new Role();
                        _newRole.Name = _role.Name;

                        _context.Roles.Add(_newRole);
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        //public bool LinkUserToCourse(int user_id, int course_id)
        //{
        //    /////
        //    var _usercourse = _context.UserCourses.Any(uc => (uc.CourseId == course_id) && (uc.UserId == user_id));
        //    if (_usercourse == true) return false;
        //    else
        //    {
        //        var _usercourse = new Usercourses()
        //        {
        //            CourseId = course_id,
        //            UserId = user_id,
        //        };
        //        _context.UserCourses.Add(_usercourse);
        //        _context.SaveChanges();
        //    }
        //    return true;
        //}


        public bool AddUserInstractor(UserInstructorAddDto model)
        {
            if (Register(model.User) == true)
            {
                var user = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.Email == model.User.Email);
                var instructor = _mapper.Map<Instructor>(model.Instructor);
                instructor.UserId = user.Id;

                _context.Instructors.Add(instructor);
                _context.SaveChanges(); return true;

            }
            return false;
        }

        public bool EditUserInstractor(int id, UserInstructorEditDto model)
        {
            var _user = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.Id == id);
            if (_user == null) return false;
            var _instructor = _context.Instructors.FirstOrDefault(i => i.UserId == id); //_mapper.Map<Instructor>(model.Instructor);
            if (_instructor == null) return false;

            else
            {
                if (model.User.Name != null) _user.Name = model.User.Name;
                if (model.User.Email != null) _user.Email = model.User.Email;
                if (model.User.UserName != null) _user.UserName = model.User.UserName;
                if (model.User.Adress != null) _user.Adress = model.User.Adress;
                if (model.User.Password != null) _user.Password = model.User.Password;
                if (model.User.Phone != null) _user.Phone = model.User.Phone;

                _context.Users.Update(_user);

                if (model.Instructor.About != null) _instructor.About = model.Instructor.About;
                if (model.Instructor.JopTitle != null) _instructor.JopTitle = model.Instructor.JopTitle;

                _context.Instructors.Update(_instructor);
            }
            _context.SaveChanges();

            return true;
        }
    }
}