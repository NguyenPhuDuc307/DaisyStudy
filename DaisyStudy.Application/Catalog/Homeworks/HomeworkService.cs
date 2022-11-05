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
using DaisyStudy.ViewModels.Catalog.Homeworks;

namespace DaisyStudy.Application.Catalog.Homeworks
{
    public class HomeworkService : IHomeworkService
    {
        private readonly DaisyStudyDbContext _context;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public HomeworkService(DaisyStudyDbContext context)
        {
            _context = context;
        }

        public async Task<int> Update(HomeworkUpdateRequest request)
        {
            var homework = await _context.Homeworks.FindAsync(request.HomeworkID);
            if (homework == null) throw new DaisyStudyException($"Cannot find a homework {request.HomeworkID}");
            homework.HomeworkName = request.HomeworkName;
            homework.Description = request.Description;
            homework.Deadline = request.Deadline;
            return await _context.SaveChangesAsync();
        }

        public async Task<HomeworkViewModel> GetById(int HomeworkID)
        {
            var homework = await _context.Homeworks.FindAsync(HomeworkID);
            if (homework == null) throw new DaisyStudyException($"Cannot find a homework {HomeworkID}");

            var _class = await _context.Classes.FindAsync(homework.ClassID);
            if (_class == null) throw new DaisyStudyException($"Cannot find a homework {HomeworkID}");

            var homeworkViewModel = new HomeworkViewModel()
            {
                HomeworkID = homework.HomeworkID,
                ClassID = _class.ClassID,
                HomeworkName = homework.HomeworkName,
                Description = homework.Description,
                DateTimeCreated = homework.DateTimeCreated,
                Deadline = homework.Deadline
            };
            return homeworkViewModel;
        }

        public async Task<int> Create(HomeworkCreateRequest request)
        {
            var homework = new Homework(){
                ClassID = request.ClassID,
                HomeworkName = request.HomeworkName,
                Description = request.Description,
                DateTimeCreated = DateTime.Now,
                Deadline = request.Deadline
            };
            _context.Homeworks.Add(homework);
            await _context.SaveChangesAsync();
            return homework.HomeworkID;
        }

        public async Task<int> Delete(int ID)
        {
            throw new NotImplementedException();
        }
    }
}

