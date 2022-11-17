using DaisyStudy.Data.EF;
using DaisyStudy.Data.Entities;
using DaisyStudy.Utilities.Exceptions;
using DaisyStudy.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using DaisyStudy.Application.Common;
using DaisyStudy.ViewModels.Catalog.StudentExams;
using Microsoft.AspNetCore.Identity;
using DaisyStudy.ViewModels.Catalog.Comments;

namespace DaisyStudy.Application.Catalog.StudentExams
{
    public class StudentExamService :IStudentExamService
    {
        private readonly DaisyStudyDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public StudentExamService(DaisyStudyDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<int> Create(StudentExamsCreateRequest request)
        {
            var student = await _userManager.FindByNameAsync(request.UserName);
            var studentexam = new StudentExam()
            {
                ExamScheduleID = request.ExamScheduleID,
                StudentID = student.Id,
                Mark = request.Mark,
                Note = request.Note,
                StudentExamDateTime = request.DateTimeStudentExam,
            };
            _context.StudentExams.Add(studentexam);
            await _context.SaveChangesAsync();
            return studentexam.StudentExamID;
        }

        public async Task<int> Delete(int StudentExamID)
        {
            var studentexam = await _context.StudentExams.FindAsync(StudentExamID);
            if (studentexam == null) throw new DaisyStudyException($"Cannot find a studentexam {StudentExamID}");

            _context.StudentExams.Remove(studentexam);
            return await _context.SaveChangesAsync();
        }

        public async Task<ApiResult<PagedResult<StudentExamsViewModel>>> GetAllPaging(GetManageStudentExamPagingRequest request)
        {
            //1. Select join
            var query = from se in _context.StudentExams
                        join u in _userManager.Users on se.StudentID equals u.Id into seu
                        from u in seu.DefaultIfEmpty()
                        select new { u, se };
            //2. filter
            if (request.ExamScheduleID != null && request.ExamScheduleID != 0)
            {
                query = query.Where(p => p.se.ExamScheduleID == request.ExamScheduleID);
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query
                .Select(x => new StudentExamsViewModel()
                {
                    StudentExamID = x.se.StudentExamID,
                    ExamScheduleID = x.se.ExamScheduleID,
                    Mark = x.se.Mark,
                    Note = x.se.Note,
                    DateTimeStudentExam = x.se.StudentExamDateTime
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<StudentExamsViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<StudentExamsViewModel>>(pagedResult);
        }

        public async Task<int> Update(StudentExamsUpdateRequest request)
        {
            var studentexam = await _context.StudentExams.FindAsync(request.StudentExamID);
            if (studentexam == null) throw new DaisyStudyException($"Cannot find a studentexam {request.StudentExamID}");
            studentexam.Mark = request.Mark;
            studentexam.ExamScheduleID = request.ExamScheduleID;
            return await _context.SaveChangesAsync();
        }

        public async Task<ApiResult<StudentExamsViewModel>> GetById(int StudentExamID)
        {
            var studentexam = await _context.StudentExams.FindAsync(StudentExamID);
            if (studentexam == null) throw new DaisyStudyException($"Cannot find a studentexam {StudentExamID}");

            var student = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == studentexam.StudentID);

            var studentexamViewModel = new StudentExamsViewModel()
            {
                StudentExamID = studentexam.StudentExamID,
                ExamScheduleID = studentexam.ExamScheduleID,
                StudentID = student.Id,
                Mark = studentexam.Mark,
                Note = studentexam.Note,
                DateTimeStudentExam = studentexam.StudentExamDateTime
            };
            return new ApiSuccessResult<StudentExamsViewModel>(studentexamViewModel);
        }
    }
}