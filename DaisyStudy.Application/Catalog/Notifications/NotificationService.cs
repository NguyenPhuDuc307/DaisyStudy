using DaisyStudy.Data.EF;
using DaisyStudy.Data.Entities;
using DaisyStudy.Utilities.Exceptions;
using DaisyStudy.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using DaisyStudy.ViewModels.Catalog.Notifications;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using DaisyStudy.Application.Common;

namespace DaisyStudy.Application.Catalog.Notifications;

public class NotificationService : INotificationService
{
    private readonly DaisyStudyDbContext _context;
    private readonly IStorageService _storageService;
    private const string USER_CONTENT_FOLDER_NAME = "user-content";
    public NotificationService(DaisyStudyDbContext context, IStorageService storageService)
    {
        _storageService = storageService;
        _context = context;
    }

    public async Task<int> Update(NotificationUpdateRequest request)
    {
        var notification = await _context.Notifications.FindAsync(request.NotificationID);
        if (notification == null) throw new DaisyStudyException($"Cannot find a notification {request.NotificationID}");
        notification.Title = request.Title;
        notification.Content = request.Content;

        //Save image
        if (request.ThumbnailImage != null)
        {
            var thumbnailImage = await _context.NotificationImages.FirstOrDefaultAsync(x => x.NotificationID == request.NotificationID);
            if (thumbnailImage != null)
            {
                thumbnailImage.ImageFileSize = request.ThumbnailImage.Length;
                thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                _context.NotificationImages.Update(thumbnailImage);
            }
        }

        return await _context.SaveChangesAsync();
    }

    public async Task<ApiResult<NotificationViewModel>> GetById(int NotificationID)
    {
        var notification = await _context.Notifications.FindAsync(NotificationID);
        if (notification == null) throw new DaisyStudyException($"Cannot find a notification {NotificationID}");

        var _class = await _context.Classes.FindAsync(notification.ClassID);
        if (_class == null) throw new DaisyStudyException($"Cannot find a class {notification.ClassID}");

        var notificationViewModel = new NotificationViewModel()
        {
            NotificationID = notification.NotificationID,
            ClassID = _class.ClassID,
            ClassName = _class.ClassName,
            Title = notification.Title,
            Content = notification.Content,
            DateTimeCreated = notification.DateTimeCreated
        };
        return new ApiSuccessResult<NotificationViewModel>(notificationViewModel);
    }

    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
        return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
    }

    public async Task<int> Create(NotificationCreateRequest request)
    {
        var notification = new Notification()
        {
            ClassID = request.ClassID,
            Title = request.Title,
            Content = request.Content,
            DateTimeCreated = DateTime.Now
        };
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
        // Save file
        if (request.ThumbnailImages != null)

            foreach (var item in request.ThumbnailImages)
            {
                var image = new NotificationImage()
                {
                    NotificationID = notification.NotificationID,
                    ImageFileSize = item.Length,
                    ImagePath = await this.SaveFile(item)
                };
                _context.NotificationImages.Add(image);
                await _context.SaveChangesAsync();
            }
        return notification.NotificationID;
    }

    public async Task<int> Delete(int NotificationID)
    {
        var notification = await _context.Notifications.FindAsync(NotificationID);
        if (notification == null) throw new DaisyStudyException($"Cannot find a notification {NotificationID}");

        _context.Notifications.Remove(notification);
        return await _context.SaveChangesAsync();
    }
    
    public async Task<ApiResult<PagedResult<NotificationViewModel>>> GetAllPaging(GetManageNotificationPagingRequest request)
    {
        //1. Select join
        var query = from n in _context.Notifications
                    join c in _context.Classes on n.ClassID equals c.ID into nc
                    from c in nc.DefaultIfEmpty()
                    select new { n, c };
        //2. filter
        if (!string.IsNullOrEmpty(request.Keyword))
            query = query.Where(x => x.n.Title.Contains(request.Keyword)
                || x.n.Content.Contains(request.Keyword));

        if (request.ClassID != null && request.ClassID != 0)
        {
            query = query.Where(p => p.n.ClassID == request.ClassID);
        }

        //3. Paging
        int totalRow = await query.CountAsync();

        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new NotificationViewModel()
            {
                NotificationID = x.n.NotificationID,
                ClassID = x.c.ClassID,
                ClassName = x.c.ClassName,
                Title = x.n.Title,
                Content = x.n.Content,
                DateTimeCreated = x.n.DateTimeCreated
            }).ToListAsync();

        //4. Select and projection
        var pagedResult = new PagedResult<NotificationViewModel>()
        {
            TotalRecords = totalRow,
            PageSize = request.PageSize,
            PageIndex = request.PageIndex,
            Items = data
        };
        return new ApiSuccessResult<PagedResult<NotificationViewModel>>(pagedResult);
    }
}

