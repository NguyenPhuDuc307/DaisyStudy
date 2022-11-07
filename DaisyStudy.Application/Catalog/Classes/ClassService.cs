using System.Data;
using Microsoft.EntityFrameworkCore;
using DaisyStudy.Data.EF;
using DaisyStudy.Data.Entities;
using DaisyStudy.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using DaisyStudy.Application.Common;
using DaisyStudy.ViewModels.Common;
using DaisyStudy.ViewModels.Catalog.ClassImages;
using DaisyStudy.Utilities.Constants;
using DaisyStudy.ViewModels.Catalog.Classes;
using Microsoft.AspNetCore.Identity;
using DaisyStudy.ViewModels.System.Users;
using Microsoft.Extensions.Configuration;

namespace DaisyStudy.Application.Catalog.Classes
{
    public class ClassService : IClassService
    {
        private readonly DaisyStudyDbContext _context;
        private readonly IStorageService _storageService;
        private readonly UserManager<AppUser> _userManager;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public ClassService(DaisyStudyDbContext context, IStorageService storageService, UserManager<AppUser> userManager)
        {
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
        }

        public async Task<int> AddImage(int ClassID, ClassImageCreateRequest request)
        {
            var classImage = new ClassImage()
            {
                ClassID = ClassID
            };

            if (request.ImageFile != null)
            {
                classImage.ImagePath = await this.SaveFile(request.ImageFile);
                classImage.ImageFileSize = request.ImageFile.Length;
            }
            _context.ClassImages.Add(classImage);
            await _context.SaveChangesAsync();
            return classImage.ImageID;
        }

        public async Task<int> UpdateImage(int imageId, ClassImageUpdateRequest request)
        {
            var productImage = await _context.ClassImages.FindAsync(imageId);
            if (productImage == null) throw new DaisyStudyException($"Cannot find an image with id {imageId}");

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.ImageFileSize = request.ImageFile.Length;
            }
            _context.ClassImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task AddViewCount(int ID)
        {
            var _class = await _context.Classes.FindAsync(ID);
            if (_class == null) throw new DaisyStudyException($"Cannot find a class {ID}");
            _class.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangeClassID(int ID)
        {
            var _class = await _context.Classes.FindAsync(ID);
            if (_class == null) throw new DaisyStudyException($"Cannot find a class {ID}");
            _class.ClassID = SystemVariable.GetRanDomClassID(7);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> Create(ClassCreateRequest request)
        {
            var _class = new Class()
            {
                ClassID = SystemVariable.GetRanDomClassID(7),
                ClassName = request.ClassName,
                Topic = request.Topic,
                ClassRoom = request.ClassRoom,
                Description = request.Description,
                SEOClassName = request.SEOClassName,
                SEODescriptione = request.SEODescription,
                SEOAlias = request.SEOAlias,
                Tuition = request.Tuition,
                DateCreated = DateTime.Now,
                ViewCount = 0,
                Status = Status.Active,
                isPublic = IsPublic.NotPublic,
            };

            // Save file
            if (request.ThumbnailImage != null)
            {
                _class.ClassImages = new List<ClassImage>()
                {
                    new ClassImage()
                    {
                        ImageFileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true
                    }
                };
            }
            _context.Classes.Add(_class);
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByNameAsync(request.UserName);

            // Add teacher
            var classDetail = new ClassDetail()
            {
                ClassID = _class.ID,
                UserID = user.Id,
                IsTeacher = Teacher.Teacher
            };
            _context.ClassDetails.Add(classDetail);
            await _context.SaveChangesAsync();

            return _class.ID;
        }

        public async Task<int> Delete(int ID)
        {
            var _class = await _context.Classes.FindAsync(ID);
            if (_class == null) throw new DaisyStudyException($"Cannot find a class {ID}");

            var images = _context.ClassImages.Where(i => i.ClassID == ID);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Classes.Remove(_class);
            return await _context.SaveChangesAsync();
        }

        public async Task<ApiResult<PagedResult<ClassViewModel>>> GetManageAllClassPaging(GetManageClassPagingRequest request)
        {
            //1. Select
            var query = from c in _context.Classes
                        join ci in _context.ClassImages on c.ID equals ci.ClassID
                        join cd in _context.ClassDetails on c.ID equals cd.ClassID
                        join us in _userManager.Users on cd.UserID equals us.Id
                        where cd.IsTeacher == Teacher.Teacher
                        select new { c, ci, cd, us };

            //2. Filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.c.ClassName.Contains(request.Keyword)
                    || x.c.ClassID.Contains(request.Keyword)
                    || x.c.Topic.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ClassViewModel()
            {
                ID = x.c.ID,
                ClassID = x.c.ClassID,
                ClassName = x.c.ClassName,
                Topic = x.c.Topic,
                ClassRoom = x.c.ClassRoom,
                Description = x.c.Description,
                SEOClassName = x.c.SEOClassName,
                SEODescriptione = x.c.SEODescriptione,
                SEOAlias = x.c.SEOAlias,
                Tuition = x.c.Tuition,
                DateCreated = x.c.DateCreated,
                ViewCount = x.c.ViewCount,
                Status = x.c.Status,
                isPublic = x.c.isPublic,
                Teacher = x.us.FirstName + " " + x.us.LastName,
                Image = "https://localhost:5001/" + x.ci.ImagePath,
                StudentNumber = _context.ClassDetails.Where(c => c.ClassID == x.c.ID).Count()

            }).ToListAsync();

            //4. Select and projection
            var pageResult = new PagedResult<ClassViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<ClassViewModel>>(pageResult);
        }

        public async Task<ApiResult<PagedResult<ClassViewModel>>> GetPublicAllClassPaging(GetManageClassPagingRequest request)
        {
            //1. Select
            var query = from c in _context.Classes
                        join ci in _context.ClassImages on c.ID equals ci.ClassID
                        join cd in _context.ClassDetails on c.ID equals cd.ClassID
                        join us in _userManager.Users on cd.UserID equals us.Id
                        where cd.IsTeacher == Teacher.Teacher && c.isPublic == IsPublic.Public
                        select new { c, ci, cd, us };

            //2. Filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.c.ClassName.Contains(request.Keyword)
                    || x.c.ClassID.Contains(request.Keyword)
                    || x.c.Topic.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ClassViewModel()
            {
                ID = x.c.ID,
                ClassID = x.c.ClassID,
                ClassName = x.c.ClassName,
                Topic = x.c.Topic,
                ClassRoom = x.c.ClassRoom,
                Description = x.c.Description,
                SEOClassName = x.c.SEOClassName,
                SEODescriptione = x.c.SEODescriptione,
                SEOAlias = x.c.SEOAlias,
                Tuition = x.c.Tuition,
                DateCreated = x.c.DateCreated,
                ViewCount = x.c.ViewCount,
                Status = x.c.Status,
                isPublic = x.c.isPublic,
                Teacher = x.us.FirstName + " " + x.us.LastName,
                Image = "https://localhost:5001/" + x.ci.ImagePath,
                StudentNumber = _context.ClassDetails.Where(c => c.ClassID == x.c.ID).Count()

            }).ToListAsync();

            //4. Select and projection
            var pageResult = new PagedResult<ClassViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<ClassViewModel>>(pageResult);
        }

        public async Task<PagedResult<ClassViewModel>> GetAll(GetPublicClassPagingRequest request)
        {
            //1. Select
            var query = from c in _context.Classes select c;

            //2. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ClassViewModel()
                {
                    ID = x.ID,
                    ClassID = x.ClassID,
                    ClassName = x.ClassName,
                    Topic = x.Topic,
                    ClassRoom = x.ClassRoom,
                    Description = x.Description,
                    SEOClassName = x.SEOClassName,
                    SEODescriptione = x.SEODescriptione,
                    SEOAlias = x.SEOAlias,
                    Tuition = x.Tuition,
                    DateCreated = x.DateCreated,
                    ViewCount = x.ViewCount,
                    Status = x.Status,
                    isPublic = x.isPublic
                }).ToListAsync();

            //3. Select and projection
            var pageResult = new PagedResult<ClassViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pageResult;
        }

        public async Task<ClassViewModel> GetById(int ID)
        {
            var _class = await _context.Classes.FindAsync(ID);
            if (_class == null) throw new DaisyStudyException($"Cannot find a class {ID}");
            var classViewModel = new ClassViewModel()
            {
                ID = _class.ID,
                ClassID = _class.ClassID,
                ClassName = _class.ClassName,
                Topic = _class.Topic,
                ClassRoom = _class.ClassRoom,
                Description = _class.Description,
                SEOClassName = _class.SEOClassName,
                SEODescriptione = _class.SEODescriptione,
                SEOAlias = _class.SEOAlias,
                Tuition = _class.Tuition,
                DateCreated = _class.DateCreated,
                ViewCount = _class.ViewCount,
                Status = _class.Status,
                isPublic = _class.isPublic
            };
            return classViewModel;
        }

        public async Task<ClassImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.ClassImages.FindAsync(imageId);
            if (image == null) throw new DaisyStudyException($"Cannot find an image with id {imageId}");
            var viewModel = new ClassImageViewModel()
            {
                ImageFileSize = image.ImageFileSize,
                ImageID = image.ImageID,
                ImagePath = image.ImagePath,
                ClassID = image.ClassID
            };
            return viewModel;
        }

        public async Task<List<ClassImageViewModel>> GetListImage(int ClassID)
        {
            return await _context.ClassImages.Where(x => x.ClassID == ClassID)
                .Select(i => new ClassImageViewModel()
                {
                    ImageFileSize = i.ImageFileSize,
                    ImageID = i.ImageID,
                    ImagePath = i.ImagePath,
                    ClassID = i.ClassID
                }).ToListAsync();
        }

        public async Task<int> RemoveImage(int imageID)
        {
            var productImage = await _context.ClassImages.FindAsync(imageID);
            if (productImage == null)
                throw new DaisyStudyException($"Cannot find an image with id {imageID}");
            _context.ClassImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ClassUpdateRequest request)
        {
            var _class = await _context.Classes.FindAsync(request.ID);
            if (_class == null) throw new DaisyStudyException($"Cannot find a class {request.ID}");
            _class.ClassName = request.ClassName;
            _class.Topic = request.Topic;
            _class.ClassRoom = request.ClassRoom;
            _class.Description = request.Description;
            _class.SEOClassName = request.SEOClassName;
            _class.SEODescriptione = request.SEODescriptione;
            _class.SEOAlias = request.SEOAlias;

            //Save image
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.ClassImages.FirstOrDefaultAsync(x => x.ClassID == request.ID);
                if (thumbnailImage != null)
                {
                    thumbnailImage.ImageFileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.ClassImages.Update(thumbnailImage);
                }
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateIsPublic(int ID, IsPublic isPublic)
        {
            var _class = await _context.Classes.FindAsync(ID);
            if (_class == null) throw new DaisyStudyException($"Cannot find a class {ID}");
            _class.isPublic = isPublic;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStatus(int ID, Status status)
        {
            var _class = await _context.Classes.FindAsync(ID);
            if (_class == null) throw new DaisyStudyException($"Cannot find a class {ID}");
            _class.Status = status;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTuition(int ID, decimal tuition)
        {
            var _class = await _context.Classes.FindAsync(ID);
            if (_class == null) throw new DaisyStudyException($"Cannot find a class {ID}");
            _class.Tuition = tuition;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<string> UploadImage(ClassImageCreateRequest request)
        {
            if (request != null)
            {
                return await this.SaveFile(request.ImageFile);
            }
            return null;
        }

        public async Task<bool> AddStudent(string ClassID, string UserName)
        {

            var _class = await _context.Classes.FirstOrDefaultAsync(x => x.ClassID == ClassID);
            if (_class == null) throw new DaisyStudyException($"Cannot find a class {ClassID}");

            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null) throw new DaisyStudyException($"Cannot find a user {UserName}");

            var classDetailCheck = _context.ClassDetails.FirstOrDefault(x => x.ClassID == _class.ID && x.UserID == user.Id);
            if (classDetailCheck != null) throw new DaisyStudyException($"Student {UserName} already exist");

            // Add student
            var classDetail = new ClassDetail()
            {
                ClassID = _class.ID,
                UserID = user.Id,
                IsTeacher = Teacher.Student
            };
            _context.ClassDetails.Add(classDetail);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<UserViewModel>> GetAllStudentByClassIDPaging(GetAllStudentInClassPagingRequest request)
        {
            //1. Select join
            var query = from st in _userManager.Users
                        join cd in _context.ClassDetails on st.Id equals cd.UserID into cdst
                        from cd in cdst.DefaultIfEmpty()
                        join c in _context.Classes on cd.ClassID equals c.ID
                        select new { cd, st, c };

            if (request.ClassID != null)
            {
                query = query.Where(x => x.cd.ClassID == request.ClassID);
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserViewModel()
                {
                    Email = x.st.Email,
                    PhoneNumber = x.st.PhoneNumber,
                    UserName = x.st.UserName,
                    FirstName = x.st.FirstName,
                    Id = x.st.Id,
                    Dob = x.st.Dob,
                    LastName = x.st.LastName
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<UserViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }
    }
}

