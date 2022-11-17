using DaisyStudy.Data.EF;
using DaisyStudy.Data.Entities;
using DaisyStudy.Application.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DaisyStudy.Utilities.Exceptions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using DaisyStudy.ViewModels.Catalog.Rooms;

namespace DaisyStudy.Application.Catalog.Rooms;

public class RoomService : IRoomService
{
    private readonly DaisyStudyDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public RoomService(DaisyStudyDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<int> Create(RoomViewModel roomViewModel)
    {
        if (_context.Rooms.Any(r => r.Name == roomViewModel.Name)) throw new DaisyStudyException("Invalid room name or room already exists");

        var user = await _userManager.FindByNameAsync(roomViewModel.UserName);
        var room = new Room()
        {
            Name = roomViewModel.Name,
            Admin = user
        };

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();

        return room.Id;
    }

    public async Task<int> Delete(int id, string userName)
    {
        var room = await _context.Rooms
                .Include(r => r.Admin)
                .Where(r => r.Id == id && r.Admin.UserName == userName)
                .FirstOrDefaultAsync();

        if (room == null) throw new DaisyStudyException($"Cannot find a room {id}");

        _context.Rooms.Remove(room);

        return await _context.SaveChangesAsync();
    }

    public async Task<int> Edit(int id, RoomViewModel roomViewModel)
    {
        if (_context.Rooms.Any(r => r.Name == roomViewModel.Name)) throw new DaisyStudyException("Invalid room name or room already exists");

        var room = await _context.Rooms
            .Include(r => r.Admin)
            .Where(r => r.Id == id && r.Admin.UserName == roomViewModel.UserName)
            .FirstOrDefaultAsync();

        if (room == null) throw new DaisyStudyException($"Cannot find a room {roomViewModel.UserName}");


        room.Name = roomViewModel.Name;

        return await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<RoomViewModel>> Get()
    {
        var rooms = await _context.Rooms
        .Select(m => new RoomViewModel()
        {
            Id = m.Id,
            Name = m.Name

        }).ToListAsync();

        return rooms;
    }

    public async Task<Room> Get(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null) throw new DaisyStudyException($"Cannot find a room {id}");

        Room roomViewModel = new Room();
        roomViewModel.Id = room.Id;
        roomViewModel.Name = room.Name;
        return roomViewModel;
    }

    public async Task<Room> Get(string Name)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(x=> x.Name == Name);
        if (room == null) throw new DaisyStudyException($"Cannot find a room {Name}");

        Room roomViewModel = new Room();
        roomViewModel.Id = room.Id;
        roomViewModel.Name = room.Name;
        return roomViewModel;
    }
}

