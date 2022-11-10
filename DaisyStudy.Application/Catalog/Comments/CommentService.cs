using DaisyStudy.Data.EF;
using DaisyStudy.Data.Entities;
using DaisyStudy.Utilities.Exceptions;
using DaisyStudy.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using DaisyStudy.Application.Common;
using DaisyStudy.ViewModels.Catalog.Comments;
using Microsoft.AspNetCore.Identity;

namespace DaisyStudy.Application.Catalog.Comments;

public class CommentService : ICommentService
{
    private readonly DaisyStudyDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IStorageService _storageService;
    private const string USER_CONTENT_FOLDER_NAME = "user-content";
    public CommentService(DaisyStudyDbContext context, IStorageService storageService, UserManager<AppUser> userManager)
    {
        _storageService = storageService;
        _context = context;
        _userManager = userManager;
    }

    public async Task<ApiResult<CommentViewModel>> GetById(int CommentID)
    {
        var comment = await _context.Comments.FindAsync(CommentID);
        if (comment == null) throw new DaisyStudyException($"Cannot find a comment {CommentID}");

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == comment.UserID);

        var commentViewModel = new CommentViewModel()
        {
            CommentID = comment.CommentID,
            NotificationID = comment.NotificationID,
            UserID = comment.UserID,
            FullName = user.FirstName + " " + user.LastName,
            Content = comment.Content,
            Likes = comment.Likes,
            Dislikes = comment.Dislikes,
            DateTimeCreated = comment.DateTimeCreated
        };
        return new ApiSuccessResult<CommentViewModel>(commentViewModel);
    }

    public async Task<int> Update(CommentUpdateRequest request)
    {
        var comment = await _context.Comments.FindAsync(request.CommentID);
        if (comment == null) throw new DaisyStudyException($"Cannot find a comment {request.CommentID}");
        comment.Content = request.Content;

        return await _context.SaveChangesAsync();
    }

    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
        return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
    }

    public async Task<int> Create(CommentCreateRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        var comment = new Comment()
        {
            NotificationID = request.NotificationID,
            UserID = user.Id,
            Content = request.Content,
            DateTimeCreated = DateTime.Now,
            Likes = 0,
            Dislikes = 0
        };
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        // Save file
        if (request.CommentImages != null)

            foreach (var item in request.CommentImages)
            {
                var image = new CommentImage()
                {
                    CommentID = comment.CommentID,
                    ImageFileSize = item.Length,
                    ImagePath = await this.SaveFile(item)
                };
                _context.CommentImages.Add(image);
                await _context.SaveChangesAsync();
            }
        return comment.CommentID;
    }

    public async Task<int> Delete(int CommentID)
    {
        var comment = await _context.Comments.FindAsync(CommentID);
        if (comment == null) throw new DaisyStudyException($"Cannot find a comment {CommentID}");

        _context.Comments.Remove(comment);
        return await _context.SaveChangesAsync();
    }

    public async Task<ApiResult<PagedResult<CommentViewModel>>> GetAllPaging(GetManageCommentPagingRequest request)
    {
        //1. Select join
        var query = from c in _context.Comments
                    join u in _userManager.Users on c.UserID equals u.Id into cu
                    from u in cu.DefaultIfEmpty()
                    select new { u, c };
        //2. filter
        if (request.NotificationID != null && request.NotificationID != 0)
        {
            query = query.Where(p => p.c.NotificationID == request.NotificationID);
        }

        //3. Paging
        int totalRow = await query.CountAsync();

        var data = await query
            .Select(x => new CommentViewModel()
            {
                CommentID = x.c.CommentID,
                NotificationID = x.c.NotificationID,
                UserID = x.c.UserID,
                FullName = x.u.FirstName + " " + x.u.LastName,
                Content = x.c.Content,
                Likes = x.c.Likes,
                Dislikes = x.c.Dislikes,
                DateTimeCreated = x.c.DateTimeCreated,
                CommentImages = (_context.CommentImages.Where(p => p.CommentID == x.c.CommentID).ToList()) != null ? (_context.CommentImages.Where(p => p.CommentID == x.c.CommentID).ToList()) : null
            }).ToListAsync();

        //4. Select and projection
        var pagedResult = new PagedResult<CommentViewModel>()
        {
            TotalRecords = totalRow,
            PageSize = request.PageSize,
            PageIndex = request.PageIndex,
            Items = data
        };
        return new ApiSuccessResult<PagedResult<CommentViewModel>>(pagedResult);
    }
}

