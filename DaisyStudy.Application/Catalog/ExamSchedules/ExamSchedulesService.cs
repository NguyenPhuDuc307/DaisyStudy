using DaisyStudy.Data.EF;
using DaisyStudy.Data.Entities;
using DaisyStudy.Utilities.Exceptions;
using DaisyStudy.ViewModels.Common;
using DaisyStudy.ViewModels.Catalog.Classes;
using DaisyStudy.ViewModels.Catalog.ExamSchedules;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace DaisyStudy.Application.Catalog.ExamSchedules;

public class ExamSchedulesService : IExamSchedulesService
{
    private readonly DaisyStudyDbContext _context;
    public ExamSchedulesService(DaisyStudyDbContext context)
    {
        _context = context;
    }

    public async Task<int> Create(ExamSchedulesCreateRequest request)
    {
        var examschedule = new ExamSchedule()
        {
            ClassID = request.ClassID,
            ExamScheduleName = request.ExamScheduleName,
            ExamDateTime = request.ExamDatetime,
            ExamTime = request.ExamTime,
            DateTimeCreated = DateTime.Now,
        };
        _context.ExamSchedules.Add(examschedule);
        await _context.SaveChangesAsync();
        return examschedule.ExamScheduleID;
    }

    public async Task<int> Delete(int ExamScheduleID)
    {
        var examschedule = await _context.ExamSchedules.FindAsync(ExamScheduleID);
        if (examschedule == null) throw new DaisyStudyException($"Cannot find a class {ExamScheduleID}");

        _context.ExamSchedules.Remove(examschedule);
        return await _context.SaveChangesAsync();
    }

    public async Task<ApiResult<PagedResult<ExamSchedulesViewModel>>> GetAllPaging(GetManageExamSchedulesPagingRequest request)
    {
        //1. Select join
        var query = from es in _context.ExamSchedules
                    join c in _context.Classes on es.ClassID equals c.ID into esc
                    from c in esc.DefaultIfEmpty()
                    select new { es, c };
        //2. filter
        if (!string.IsNullOrEmpty(request.Keyword))
            query = query.Where(x => x.es.ExamScheduleName.Contains(request.Keyword)
                || x.c.ClassID.Contains(request.Keyword));

        if (request.ClassID != null && request.ClassID != 0)
        {
            query = query.Where(p => p.es.ClassID == request.ClassID);
        }

        //3. Paging
        int totalRow = await query.CountAsync();

        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ExamSchedulesViewModel()
            {
                ExamScheduleID = x.es.ExamScheduleID,
                ClassID = x.c.ClassID,
                ExamScheduleName = x.es.ExamScheduleName,
                DateTimeCreated = x.es.DateTimeCreated,
                ExamDatetime = x.es.ExamDateTime,
                ExamTime = x.es.ExamTime
            }).ToListAsync();

        //4. Select and projection
        var pagedResult = new PagedResult<ExamSchedulesViewModel>()
        {
            TotalRecords = totalRow,
            PageSize = request.PageSize,
            PageIndex = request.PageIndex,
            Items = data
        };
        return new ApiSuccessResult<PagedResult<ExamSchedulesViewModel>>(pagedResult);
    }

    public async Task<ExamSchedulesViewModel> GetById(int ExamcheduleID)
    {
        var examschedules = await _context.ExamSchedules.FindAsync(ExamcheduleID);
        if (examschedules == null) throw new DaisyStudyException($"Cannot find a examschedules {ExamcheduleID}");

        var _class = await _context.Classes.FindAsync(examschedules.ClassID);
        if (_class == null) throw new DaisyStudyException($"Cannot find a class {examschedules.ClassID}");

        var examschedulesViewModel = new ExamSchedulesViewModel()
        {
            ExamScheduleID = examschedules.ExamScheduleID,
            ClassID = _class.ClassID,
            ClassName = _class.ClassName,
            ExamScheduleName = examschedules.ExamScheduleName,
            DateTimeCreated = examschedules.DateTimeCreated,
            ExamDatetime = examschedules.ExamDateTime,
            ExamTime = examschedules.ExamTime
        };
        return examschedulesViewModel;  
    }

    public async Task<int> Update(ExamSchedulesUpdateRequest request)
    {
        var examschedule = await _context.ExamSchedules.FindAsync(request.ExamScheduleID);
        if (examschedule == null) throw new DaisyStudyException($"Cannot find a examschedule {request.ExamScheduleID}");
        examschedule.ExamScheduleName = request.ExamScheduleName;
        examschedule.ExamDateTime = request.ExamDatetime;
        examschedule.ExamTime = request.ExamTime;
        return await _context.SaveChangesAsync();
    }
}
