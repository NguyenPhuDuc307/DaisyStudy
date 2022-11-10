using DaisyStudy.Data.EF;
using DaisyStudy.Data.Entities;
using DaisyStudy.Utilities.Exceptions;
using DaisyStudy.ViewModels.Common;
using DaisyStudy.ViewModels.Catalog.Classes;
using DaisyStudy.ViewModels.Catalog.Homeworks;
using Microsoft.EntityFrameworkCore;

namespace DaisyStudy.Application.Catalog.Homeworks;

public class HomeworkService : IHomeworkService
{
    private readonly DaisyStudyDbContext _context;
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
        if (_class == null) throw new DaisyStudyException($"Cannot find a class {homework.ClassID}");

        var homeworkViewModel = new HomeworkViewModel()
        {
            HomeworkID = homework.HomeworkID,
            ClassID = _class.ClassID,
            ClassName = _class.ClassName,
            HomeworkName = homework.HomeworkName,
            Description = homework.Description,
            DateTimeCreated = homework.DateTimeCreated,
            Deadline = homework.Deadline
        };
        return homeworkViewModel;
    }

    public async Task<int> Create(HomeworkCreateRequest request)
    {
        var homework = new Homework()
        {
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

    public async Task<int> Delete(int HomeworkID)
    {
        var homework = await _context.Homeworks.FindAsync(HomeworkID);
        if (homework == null) throw new DaisyStudyException($"Cannot find a class {HomeworkID}");

        _context.Homeworks.Remove(homework);
        return await _context.SaveChangesAsync();
    }

    public async Task<ApiResult<PagedResult<HomeworkViewModel>>> GetAllPaging(GetManageHomeworkPagingRequest request)
    {
        //1. Select join
        var query = from hw in _context.Homeworks
                    join c in _context.Classes on hw.ClassID equals c.ID into hwc
                    from c in hwc.DefaultIfEmpty()
                    select new { hw, c };
        //2. filter
        if (!string.IsNullOrEmpty(request.Keyword))
            query = query.Where(x => x.hw.HomeworkName.Contains(request.Keyword)
                || x.c.ClassID.Contains(request.Keyword));

        if (request.ClassID != null && request.ClassID != 0)
        {
            query = query.Where(p => p.hw.ClassID == request.ClassID);
        }

        //3. Paging
        int totalRow = await query.CountAsync();

        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new HomeworkViewModel()
            {
                HomeworkID = x.hw.HomeworkID,
                ClassID = x.c.ClassID,
                ClassName = x.c.ClassName,
                HomeworkName = x.hw.HomeworkName,
                Description = x.hw.Description,
                DateTimeCreated = x.hw.DateTimeCreated,
                Deadline = x.hw.Deadline
            }).ToListAsync();

        //4. Select and projection
        var pagedResult = new PagedResult<HomeworkViewModel>()
        {
            TotalRecords = totalRow,
            PageSize = request.PageSize,
            PageIndex = request.PageIndex,
            Items = data
        };
        return new ApiSuccessResult<PagedResult<HomeworkViewModel>>(pagedResult);
    }
}

