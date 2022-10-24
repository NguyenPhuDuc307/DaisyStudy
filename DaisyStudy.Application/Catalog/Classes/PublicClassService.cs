using DaisyStudy.Data.EF;
using DaisyStudy.ViewModel.Common;
using Microsoft.EntityFrameworkCore;

namespace DaisyStudy.ViewModel.Catalog.Classes;

public class PublicClassService : IPublicClassService
{
    private readonly DaisyStudyDbContext _context;

    public PublicClassService(DaisyStudyDbContext context)
    {
        _context = context;
    }
    public async Task<PagedResult<ClassViewModel>> GetAll(GetClassPagingRequest request)
    {
        //1. Select
        var query = from c in _context.Classes select c;

        //2. Filter

        //3. Paging
        int totalRow = await query.CountAsync();
        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
        .Take(request.PageSize)
        .Select(x => new ClassViewModel()
        {
            ID = x.ID,
            ClassName = x.ClassName,
            Topic = x.Topic,
            Image = x.Image,
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

        //4. Select and projection
        var pageResult = new PagedResult<ClassViewModel>()
        {
            TotalRecord = totalRow,
            Items = data
        };
        return pageResult;
    }
}
