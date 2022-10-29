using DaisyStudy.Data.EF;
using DaisyStudy.ViewModels.Catalog.Classes;
using DaisyStudy.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace DaisyStudy.Application.Catalog.Classes;

public class PublicClassService : IPublicClassService
{
    private readonly DaisyStudyDbContext _context;

    public PublicClassService(DaisyStudyDbContext context)
    {
        _context = context;
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
            TotalRecord = totalRow,
            Items = data
        };
        return pageResult;
    }
}
