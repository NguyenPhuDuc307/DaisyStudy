using DaisyStudy.Data.EF;
using DaisyStudy.Data.Entities;
using DaisyStudy.Application.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DaisyStudy.ViewModels.Catalog.RoomChats;
using Microsoft.EntityFrameworkCore;
using DaisyStudy.ViewModels.Common;
using DaisyStudy.Utilities.Exceptions;

namespace DaisyStudy.Application.Catalog.RoomChats
{
    public class RoomChatService : IRoomChatService
    {
        private readonly DaisyStudyDbContext _context;
        private readonly IStorageService _storageService;
        private readonly UserManager<AppUser> _userManager;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public RoomChatService(DaisyStudyDbContext context, IStorageService storageService, UserManager<AppUser> userManager)
        {
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
        }

        public async Task<List<RoomChatViewModel>> GetAll()
        {
            var query = _context.RoomChats;

            var data = await query.Select(x => new RoomChatViewModel()
            {
                Id = x.RoomChatID,
                Name = x.RoomChatName
            }).ToListAsync();

            return data;
        }

        public async Task<RoomChat> GetByName(string RoomChatName)
        {
            var roomChat = await _context.RoomChats.FirstOrDefaultAsync(r=> r.RoomChatName == RoomChatName);

            if (roomChat == null) throw new DaisyStudyException($"Cannot find a room {RoomChatName}");

            return roomChat;
        }

        public async Task<RoomChat> GetById(int RoomChatID)
        {
            var roomChat = await _context.RoomChats.FindAsync(RoomChatID);

            if (roomChat == null) throw new DaisyStudyException($"Cannot find a room {RoomChatID}");

            return roomChat;
        }

        public async Task<int> Create(RoomChatViewModel request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) throw new DaisyStudyException($"Cannot find a user {request.UserName}");
            var roomChat = new RoomChat()
            {
                RoomChatName = request.Name,
                AdminID = user.Id
            };
            _context.RoomChats.Add(roomChat);
            await _context.SaveChangesAsync();
            return roomChat.RoomChatID;
        }

        public async Task<int> Update(int RoomChatID, RoomChatViewModel request)
        {
            var roomChat = await _context.RoomChats.FindAsync(RoomChatID);
            if (roomChat == null) throw new DaisyStudyException($"Cannot find a room {RoomChatID}");

            roomChat.RoomChatName = request.Name;
            await _context.SaveChangesAsync();
            return roomChat.RoomChatID;
        }

        public async Task<int> Delete(int RoomChatID)
        {
            var roomChat = await _context.RoomChats.FindAsync(RoomChatID);
            if (roomChat == null) throw new DaisyStudyException($"Cannot find a room {RoomChatID}");
            _context.RoomChats.Remove(roomChat);
            await _context.SaveChangesAsync();
            return roomChat.RoomChatID;
        }
    }
}

