using DaisyStudy.Data.EF;
using DaisyStudy.Data.Entities;
using DaisyStudy.Utilities.Exceptions;
using DaisyStudy.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using DaisyStudy.Application.Common;
using DaisyStudy.ViewModels.Catalog.Chats;
using Microsoft.AspNetCore.Identity;

namespace DaisyStudy.Application.Catalog.Chats;

public class ChatService : IChatService
{
    private readonly DaisyStudyDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IStorageService _storageService;
    private const string USER_CONTENT_FOLDER_NAME = "user-content";
    public ChatService(DaisyStudyDbContext context, IStorageService storageService, UserManager<AppUser> userManager)
    {
        _storageService = storageService;
        _context = context;
        _userManager = userManager;
    }

    public async Task<ApiResult<ChatViewModel>> GetById(int ChatID)
    {
        var chat = await _context.Chats.FindAsync(ChatID);
        if (chat == null) throw new DaisyStudyException($"Cannot find a chat {ChatID}");

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == chat.UserID);

        var ChatViewModel = new ChatViewModel()
        {
            ChatID = chat.ChatID,
            ClassID = chat.ClassID,
            UserID = chat.UserID,
            Avatar = user.Avatar,
            FullName = user.FirstName + " " + user.LastName,
            Content = chat.Content,
            DateTimeCreated = chat.DateTimeCreated,
            Likes = chat.Likes,
            Dislikes = chat.Dislikes
        };
        return new ApiSuccessResult<ChatViewModel>(ChatViewModel);
    }

    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
        return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
    }

    public async Task<int> Create(ChatCreateRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        var chat = new Chat()
        {
            ClassID = request.ClassID,
            UserID = user.Id,
            Content = request.Content,
            DateTimeCreated = DateTime.Now
        };
        _context.Chats.Add(chat);
        await _context.SaveChangesAsync();
        // Save file
        if (request.ChatImages != null)

            foreach (var item in request.ChatImages)
            {
                var image = new ChatImage()
                {
                    ChatID = chat.ChatID,
                    ImageFileSize = item.Length,
                    ImagePath = await this.SaveFile(item)
                };
                _context.ChatImages.Add(image);
                await _context.SaveChangesAsync();
            }
        return chat.ChatID;
    }

    public async Task<int> Delete(int ChatID)
    {
        var chat = await _context.Chats.FindAsync(ChatID);
        if (chat == null) throw new DaisyStudyException($"Cannot find a chat {ChatID}");

        _context.Chats.Remove(chat);
        return await _context.SaveChangesAsync();
    }

    public async Task<ApiResult<PagedResult<ChatViewModel>>> GetAllPaging(GetManageChatPagingRequest request)
    {
        //1. Select join
        var query = from c in _context.Chats
                    join u in _userManager.Users on c.UserID equals u.Id into cu
                    from u in cu.DefaultIfEmpty()
                    select new { u, c };
        //2. filter
        if (request.ClassID != null && request.ClassID != 0)
        {
            query = query.Where(p => p.c.ClassID == request.ClassID);
        }

        //3. Paging
        int totalRow = await query.CountAsync();

        var data = await query
            .Select(x => new ChatViewModel()
            {
                ChatID = x.c.ChatID,
                ClassID = x.c.ClassID,
                UserID = x.c.UserID,
                Avatar = x.u.Avatar,
                FullName = x.u.FirstName + " " + x.u.LastName,
                Content = x.c.Content,
                DateTimeCreated = x.c.DateTimeCreated,
                Likes = x.c.Likes,
                Dislikes = x.c.Dislikes,
                ChatImages = (_context.ChatImages.Where(p => p.ChatID == x.c.ChatID).ToList()) != null ? (_context.ChatImages.Where(p => p.ChatID == x.c.ChatID).ToList()) : null
            }).ToListAsync();

        //4. Select and projection
        var pagedResult = new PagedResult<ChatViewModel>()
        {
            TotalRecords = totalRow,
            PageSize = request.PageSize,
            PageIndex = request.PageIndex,
            Items = data
        };
        return new ApiSuccessResult<PagedResult<ChatViewModel>>(pagedResult);
    }
}

