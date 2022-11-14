using DaisyStudy.Data.EF;
using DaisyStudy.Data.Entities;
using DaisyStudy.Application.Common;
using Microsoft.AspNetCore.Identity;
using DaisyStudy.ViewModels.Catalog.Messages;
using DaisyStudy.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace DaisyStudy.Application.Catalog.Messages
{
    public class MessageService : IMessageService
    {
        private readonly DaisyStudyDbContext _context;
        private readonly IStorageService _storageService;
        private readonly UserManager<AppUser> _userManager;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public MessageService(DaisyStudyDbContext context, IStorageService storageService, UserManager<AppUser> userManager)
        {
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
        }

        public async Task<Message> GetById(int MessageID)
        {
            var message = await _context.Messages.FindAsync(MessageID);

            if (message == null) throw new DaisyStudyException($"Cannot find a message {MessageID}");

            return message;
        }

        public async Task<int> Create(MessageViewModel request)
        {
            var user = await _userManager.FindByNameAsync(request.From);
            if (user == null) throw new DaisyStudyException($"Cannot find a user {request.From}");

            var roomChat = _context.RoomChats.FirstOrDefault(r=> r.RoomChatName == request.RoomChatName);

            if (roomChat == null) throw new DaisyStudyException($"Cannot find a room {request.RoomChatName}");

            var message = new Message()
            {
                FromUserID = user.Id,
                ToRoomID = roomChat.RoomChatID,
                Content = request.Content,
                TimeStamp = DateTime.Now
            };
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message.MessageID;
        }

        public async Task<int> Delete(int MessageID)
        {
            var message = await _context.Messages.FindAsync(MessageID);
            if (message == null) throw new DaisyStudyException($"Cannot find a message {MessageID}");
            _context.Messages.Remove(message);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<MessageViewModel>> GetAll(string roomName)
        {
            var roomChat = _context.RoomChats.FirstOrDefault(r=> r.RoomChatName == roomName);

            var data = _context.Messages.Where(m=> m.ToRoomID == roomChat.RoomChatID)
            .Take(20)
            .Select(m=> new MessageViewModel(){
                Content = m.Content,
                TimeStamp = m.TimeStamp,
                From = m.FromUser.LastName,
                RoomChatName = m.ToRoom.RoomChatName,
                Avatar = m.FromUser.Avatar

            }).OrderByDescending(m => m.TimeStamp)
                .AsEnumerable()
                .Reverse()
                .ToList();

            return data;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<int> UploadFile(UploadViewModel request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) throw new DaisyStudyException($"Cannot find a user {request.UserName}");

            var message = new Message()
            {
                FromUserID = user.Id,
                ToRoomID = request.RoomId,
                Content = await this.SaveFile(request.File),
                TimeStamp = DateTime.Now
            };
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message.MessageID;
        }
    }
}

