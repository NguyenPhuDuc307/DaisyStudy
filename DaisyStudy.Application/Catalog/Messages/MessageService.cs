using DaisyStudy.Data.EF;
using DaisyStudy.Data.Entities;
using DaisyStudy.Application.Common;
using Microsoft.AspNetCore.Identity;
using DaisyStudy.ViewModels.Catalog.Messages;
using DaisyStudy.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using DaisyStudy.ViewModels.Catalog.Uploads;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using DaisyStudy.Application.Common.Helpers;

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

        public async Task<Message> Get(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null) throw new DaisyStudyException($"Cannot find a message {id}");

            Message messageViewModel = new Message();
            messageViewModel.Id = message.Id;
            messageViewModel.Content = message.Content;
            messageViewModel.Timestamp = message.Timestamp;
            messageViewModel.ToRoomId = message.ToRoomId;
            return messageViewModel;
        }

        public async Task<IEnumerable<MessageViewModel>> GetMessages(string roomName)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Name == roomName);
            if (room == null) throw new DaisyStudyException($"Cannot find a room {roomName}");

            var query = from m in _context.Messages where m.ToRoomId == room.Id select new { m };

            var messagesViewModels = await query
            .Select(x => new MessageViewModel()
            {
                Id = x.m.Id,
                Content = x.m.Content,
                Timestamp = x.m.Timestamp,
                Room = x.m.ToRoom.Name,
                From = x.m.FromUser.FirstName + " "+ x.m.FromUser.LastName,
                Avatar = x.m.FromUser.Avatar,
                UserName = x.m.FromUser.UserName
            }).ToListAsync();
            
            return messagesViewModels;
        }

        public async Task<MessageViewModel> Create(MessageViewModel messageViewModel)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == messageViewModel.UserName);
            var room = _context.Rooms.FirstOrDefault(r => r.Name == messageViewModel.Room);
            if (room == null) throw new DaisyStudyException($"Cannot find a room {messageViewModel.Room}");

            var msg = new Message()
            {
                Content = BasicEmojis.ParseEmojis( Regex.Replace(messageViewModel.Content, @"<.*?>", string.Empty)),
                FromUser = user,
                ToRoom = room,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(msg);
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
        
        public async Task<int> Delete(int id, string UserName)
        {
            var message = await _context.Messages
                .Include(u => u.FromUser)
                .Where(m => m.Id == id && m.FromUser.UserName == UserName)
                .FirstOrDefaultAsync();

            if (message == null) throw new DaisyStudyException($"Cannot find a message {id}");

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return id;
        }
    }
}

