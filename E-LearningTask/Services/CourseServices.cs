using AutoMapper;
using DAL.Data;
using DAL.DTO;
using DAL.Entities;
using E_LearningTask.Services.Helper;
using E_LearningTask.Services.Interfaces;

namespace E_LearningTask.Services
{
    public class CourseServices : ICourseServices
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDBContext _context;
        private readonly IExtension _extension;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CourseServices(IMapper mapper, ApplicationDBContext context, IExtension extension, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _context = context;
            _extension = extension;
            _webHostEnvironment = webHostEnvironment;
        }

        public bool AddCourse(CourseAddDto model)
        {
            /////
            var course = _mapper.Map<Course>(model);
            try
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }


        public bool EditCourse(int id, CourseEditDto model)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return false;
            else
            {
                if (model.Name != null) course.Name = model.Name;
                if (model.Description != null) course.Description = model.Description;
                if (model.Price != null) course.Price = model.Price;
                if (model.InstructorId != null) course.InstructorId = model.InstructorId;
                if (model.CategoryId != null) course.CategoryId = model.CategoryId;
            }
            _context.Courses.Update(course);
            _context.SaveChanges();
            return true;
        }


        public bool AddImageAndFile(int id, CourseAccessFilesDto model)
        {
            ///
            string imagePath = "";
            string pdfPath = "";

            string rootpath = _webHostEnvironment.ContentRootPath;
            string folderImageName = "/Files/Courses/";
            string folderPdfName = "/Files/Courses/";
            var course = _context.Courses.Find(id);
            if (course == null) return false;
            else
            {
                folderImageName = "/Files/" + course.Name + "/Images/";
                folderPdfName = "/Files/" + course.Name + "/Pdf/";
            }

            if (model.Image != null)
            {
                var imageext = System.IO.Path.GetExtension(model.Image.FileName);
                var allowedImageExt = new List<string> { ".png", "jpeg" };
                if (!allowedImageExt.Contains(imageext.ToLower()))
                {
                    return false; //("Extension not allowed");  //>>NEED TO HANDELED
                }
                else
                {
                    imagePath = _extension.UploudFile(rootpath, folderImageName, model.Image);
                    course.ImageUrl = rootpath + folderImageName;
                    ///Another table load all images
                    _context.Courses.Update(course);
                }
            }

            if (model.Pdf != null)
            {
                var pdfext = System.IO.Path.GetExtension(model.Pdf.FileName);
                if (pdfext != ".pdf")
                {
                    return false; //("Extension not allowed");  //>>NEED TO HANDELED
                }
                else
                {
                    pdfPath = _extension.UploudFile(rootpath, folderPdfName, model.Pdf);
                    course.PdfUrl = rootpath + folderPdfName;
                    ///Another table load all pdf
                    _context.Courses.Update(course);
                }
            }
            _context.SaveChanges();

            return true;
        }


        public bool AddRate(int id, UserCourseRateDto model)
        {
            ///check user and course
            var _userCourse = _context.UserCourses.Find(id, model.UserId);
            if (_userCourse == null) { return false; }

            _userCourse.SEvaluation = model.SEvaluation;
            _userCourse.ReviewText = model.ReviewText;
            try
            {
                _context.UserCourses.Update(_userCourse);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }


            return true;
        }

        public List<CourseGetDetailDto> GetAllCoursesDetil()
        {
            var res = new List<CourseGetDetailDto>();
            var res_Playlists = new List<PlayListGetDto>();

            var _courses = _context.Courses.ToList();

            if (_courses == null)
            {
                return null;
            }


            foreach (var _course in _courses)
            {
                var _instructor = _context.Instructors.Find(_course.InstructorId);
                var _category = _context.Categories.Find(_course.CategoryId);
                var _playLists = _context.PlayLists.Where(p => p.CourseId == _course.Id).ToList();

                var _courseDto = _mapper.Map<CourseGetDetailDto>(_course);

                var _instructorDto = _mapper.Map<InstructorGetDto>(_instructor);
                _courseDto.Instructor = _instructorDto;
                var _categoryDto = _mapper.Map<CategoryGetDto>(_category);
                _courseDto.Category = _categoryDto;

                foreach (var _playlist in _playLists)
                {
                    var _playlistDto = _mapper.Map<PlayListGetDto>(_playlist);
                    res_Playlists.Add(_playlistDto);
                }
                _courseDto.PlayLists = res_Playlists;
                res.Add(_courseDto);
            }


            return res;
        }
    }
}
