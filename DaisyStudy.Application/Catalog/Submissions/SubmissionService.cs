using DaisyStudy.Data.EF;
using DaisyStudy.Data.Entities;
using DaisyStudy.Utilities.Exceptions;
using DaisyStudy.ViewModels.Catalog.Submissions;
using DaisyStudy.ViewModels.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DaisyStudy.Application.Catalog.Submissions;

public class SubmissionService : ISubmissionService
{
    private readonly DaisyStudyDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private const string USER_CONTENT_FOLDER_NAME = "user-content";

    public SubmissionService(DaisyStudyDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<ApiResult<bool>> Create(SubmissionCreateRequest request)
    {
        var homework = await _context.Homeworks.FindAsync(request.HomeworkID);
        if (homework == null) throw new DaisyStudyException($"Cannot find a homework {request.HomeworkID}");

        Delay delay = Delay.Delay;
        if (DateTime.Now > homework.Deadline)
        {
            delay = Delay.NotDelay;
        }

        var submission = new Submission()
        {
            HomeworkID = request.HomeworkID,
            StudentID = request.StudentID,
            Description = request.Description,
            SubmissionDateTime = DateTime.Now,
            Delay = delay
        };
        _context.Submissions.Add(submission);
        var result = await _context.SaveChangesAsync();
        if (result > 0)
        {
            return new ApiSuccessResult<bool>();
        }
        return new ApiErrorResult<bool>("Nộp bài không thành công");
    }

    public async Task<ApiResult<bool>> Update(SubmissionUpdateRequest request)
    {
        var submission = _context.Submissions.FirstOrDefault(x => x.HomeworkID == request.HomeworkID && x.StudentID == request.StudentID);
        if (submission == null) throw new DaisyStudyException($"Cannot find a submission {request.HomeworkID}");
        submission.Description = request.Description;
        submission.DateTimeUpdated = DateTime.Now;
        var result = await _context.SaveChangesAsync();
        if (result > 0)
        {
            return new ApiSuccessResult<bool>();
        }
        return new ApiErrorResult<bool>("Cập nhật không thành công");
    }

    public async Task<ApiResult<bool>> Delete(int HomeworkID, string UserName)
    {
        var student = await _userManager.FindByNameAsync(UserName);

        var submission = _context.Submissions.FirstOrDefault(x => x.HomeworkID == HomeworkID && x.StudentID == student.Id);
        if (submission == null) throw new DaisyStudyException($"Cannot find a submission {HomeworkID}");

        _context.Submissions.Remove(submission);
        var result = await _context.SaveChangesAsync();
        if (result > 0)
        {
            return new ApiSuccessResult<bool>();
        }
        return new ApiErrorResult<bool>("Xoá bài nộp không thành công");
    }

    public async Task<SubmissionViewModel> GetById(int HomeworkID, string UserName)
    {
        var student = await _userManager.FindByNameAsync(UserName);

        var submission = _context.Submissions.FirstOrDefault(x => x.HomeworkID == HomeworkID && x.StudentID == student.Id);
        if (submission == null) throw new DaisyStudyException($"Cannot find a submission {HomeworkID}");

        var homework = await _context.Homeworks.FindAsync(submission.HomeworkID);
        if (homework == null) throw new DaisyStudyException($"Cannot find a homework {HomeworkID}");

        var _class = await _context.Classes.FindAsync(homework.ClassID);
        if (_class == null) throw new DaisyStudyException($"Cannot find a class {homework.ClassID}");

        var homeworkViewModel = new SubmissionViewModel()
        {
            HomeworkID = homework.HomeworkID,
            StudentID = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            PhoneNumber = student.PhoneNumber,
            Email = student.Email,
            HomeworkName = homework.HomeworkName,
            ClassID = _class.ClassID,
            ClassName = _class.ClassName,
            Description = homework.Description,
            DateTimeCreated = homework.DateTimeCreated,
            Deadline = homework.Deadline,
            Mark = submission.Mark,
            Note = submission.Note,
            DescriptionSubmission = submission.Description,
            SubmissionDateTime = submission.SubmissionDateTime,
            DateTimeUpdated = submission.DateTimeUpdated,
            Delay = submission.Delay
        };
        return homeworkViewModel;
    }

    public async Task<PagedResult<SubmissionViewModel>> GetAllPaging(GetManageSubmissionPagingRequest request)
    {
        //1. Select join
        var query = from sm in _context.Submissions
                    join st in _userManager.Users on sm.StudentID equals st.Id into smst
                    from st in smst.DefaultIfEmpty()
                    join hw in _context.Homeworks on sm.HomeworkID equals hw.HomeworkID into smhw
                    from hw in smhw.DefaultIfEmpty()
                    join c in _context.Classes on hw.ClassID equals c.ID
                    where sm.HomeworkID == request.HomeworkID
                    select new { sm, st, hw, c };
        //2. filter
        if (request.Delay == Delay.Delay)
            query = query.Where(x => x.sm.Delay == Delay.Delay);

        if (request.Delay == Delay.NotDelay)
            query = query.Where(x => x.sm.Delay == Delay.NotDelay);


        //3. Paging
        int totalRow = await query.CountAsync();

        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new SubmissionViewModel()
            {
                HomeworkID = x.hw.HomeworkID,
                StudentID = x.st.Id,
                FirstName = x.st.FirstName,
                LastName = x.st.LastName,
                PhoneNumber = x.st.PhoneNumber,
                Email = x.st.Email,
                HomeworkName = x.hw.HomeworkName,
                ClassID = x.c.ClassID,
                ClassName = x.c.ClassName,
                Description = x.hw.Description,
                DateTimeCreated = x.hw.DateTimeCreated,
                Deadline = x.hw.Deadline,
                Mark = x.sm.Mark,
                Note = x.sm.Note,
                DescriptionSubmission = x.sm.Description,
                SubmissionDateTime = x.sm.SubmissionDateTime,
                DateTimeUpdated = x.sm.DateTimeUpdated,
                Delay = x.sm.Delay
            }).ToListAsync();

        //4. Select and projection
        var pagedResult = new PagedResult<SubmissionViewModel>()
        {
            TotalRecords = totalRow,
            PageSize = request.PageSize,
            PageIndex = request.PageIndex,
            Items = data
        };
        return pagedResult;
    }
}

