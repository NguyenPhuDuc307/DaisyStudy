using DaisyStudy.Data.EF;
using DaisyStudy.Data.Entities;
using DaisyStudy.Application.Common;
using Microsoft.AspNetCore.Identity;
using DaisyStudy.ViewModels.Catalog.Messages;
using DaisyStudy.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using DaisyStudy.ViewModels.Catalog.Uploads;

namespace DaisyStudy.Application.Catalog.Uploads;

public class UploadImageService : IUploadImageService
{
    private readonly DaisyStudyDbContext _context;
    private readonly IStorageService _storageService;
    private readonly UserManager<AppUser> _userManager;
    private const string USER_CONTENT_FOLDER_NAME = "user-content";

    public UploadImageService(DaisyStudyDbContext context, IStorageService storageService, UserManager<AppUser> userManager)
    {
        _context = context;
        _storageService = storageService;
        _userManager = userManager;
    }

    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
        return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
    }

    public async Task<MessageViewModel> Upload(UploadViewModel uploadViewModel)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserName == uploadViewModel.UserName);
        var room = _context.Rooms.FirstOrDefault(r => r.Id == uploadViewModel.RoomId);
        if (room == null) throw new DaisyStudyException($"Cannot find a room {uploadViewModel.RoomId}");

        string htmlImage = string.Format(
                    "<a href=\"https://localhost:5001/{0}\" target=\"_blank\">" +
                    "<img src=\"https://localhost:5001/{0}\" class=\"post-image\">" +
                    "</a>", await this.SaveFile(uploadViewModel.File));

        var msg = new Message()
        {
            Content = Regex.Replace(htmlImage, @"(?i)<(?!img|a|/a|/img).*?>", string.Empty),
            FromUser = user,
            ToRoom = room,
            Timestamp = DateTime.Now
        };

        await _context.Messages.AddAsync(msg);
        await _context.SaveChangesAsync();

        // Broadcast the message
        MessageViewModel createdMessage = new MessageViewModel();
        createdMessage.Id = msg.Id;
        createdMessage.Content = msg.Content;
        createdMessage.Timestamp = msg.Timestamp;
        createdMessage.Room = room.Name;
        createdMessage.From = user.FirstName + " " + user.LastName;

        return createdMessage;
    }
}

