using AutoMapper;
using DAL.Data;
using DAL.DTO;
using DAL.Entities;
using E_LearningTask.Services.Helper;
using E_LearningTask.Services.Interfaces;

namespace E_LearningTask.Services
{
    public class LessonServices : ILessonServices
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDBContext _context;
        private readonly IExtension _extension;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LessonServices(ApplicationDBContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment, IExtension extension)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _extension = extension;
        }

        public bool EditLessonData(int id, MediaEditDataDto model)
        {
            var lesson = _context.Lessons.Find(id);
            if (lesson == null) return false;

            var _medias = _context.Medias.Where(m => m.LessonId == id).ToList();
            if (_medias == null) return false;

            try
            {
                foreach (var media in _medias)
                {
                    ///
                    if (model.Description != null) media.Description = model.Description;
                    if (model.IsPublic != null) media.IsPublic = model.IsPublic;
                    if (model.MediaRefer != null) media.MediaRefer = model.MediaRefer;
                    if (model.MediaType != null) media.MediaType = model.MediaType;

                    _context.Medias.Update(media);
                }
                _context.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }

            //   _context.SaveChanges();
            return true;
        }


        public LessonGetDataDto GetLessonDetails(int id)
        {
            var lesson = _context.Lessons.Find(id);
            if (lesson == null) return null;

            var _lessonGetDataDto = _mapper.Map<LessonGetDataDto>(lesson);
            ////
            var _dataUrl = _context.Medias.Where(m => m.LessonId == id)
                                          .Select(m => m.Url).ToList();

            var _playList = _context.PlayLists.Where(m => m.Id == lesson.PlayListId).FirstOrDefault();

            _lessonGetDataDto.PlayList = _mapper.Map<PlayListGetDto>(_playList);

            _lessonGetDataDto.Data = _dataUrl;

            /////
            return _lessonGetDataDto;
        }


        public bool AddLessonData(int id, MediaAddTypeDto model)
        {
            string imagePath = "";

            string rootpath = _webHostEnvironment.ContentRootPath;
            string folderImageName = "/Files/Courses/PlayList/Lesson";
            var allowedImageExt = new List<string> { ".png", "jpeg", ".pdf" };


            var lesson = _context.Lessons.Find(id);

            if (lesson == null) { return false; }
            //////id
            ///
            else
            {
                folderImageName = "/Files/Courses/PlayList/" + lesson.Name;  // + "/Images/";
            }

            if (model.Files == null) { return false; }
            //////
            ////
            try
            {
                foreach (var item in model.Files)
                {
                    /////
                    ///
                    var media = _mapper.Map<Media>(model);
                    media.LessonId = id;

                    var imageext = System.IO.Path.GetExtension(item.FileName);
                    if (!allowedImageExt.Contains(imageext.ToLower()))
                    {
                        return false; //("Extension not allowed");  //>>NEED TO HANDELED
                    }
                    else
                    {
                        imagePath = _extension.UploudFile(rootpath, folderImageName, item);
                        media.Url = rootpath + imagePath;
                    }
                    _context.Medias.Add(media);
                }
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool AddLesson(LessonAddDto model)
        {
            var lesson = _mapper.Map<Lesson>(model);

            try
            {
                _context.Lessons.Add(lesson);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
