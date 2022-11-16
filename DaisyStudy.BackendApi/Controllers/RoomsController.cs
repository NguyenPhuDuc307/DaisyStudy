using DaisyStudy.Application.Catalog.Rooms;
using DaisyStudy.BackendApi.Hubs;
using DaisyStudy.Data.EF;
using DaisyStudy.Data.Entities;
using DaisyStudy.ViewModels.Catalog.Rooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DaisyStudy.BackendApi.Controllers;

// api/rooms
// [Authorize]
[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;
    private readonly IHubContext<ChatHub> _hubContext;

    public RoomsController(IRoomService roomService, IHubContext<ChatHub> hubContext)
    {
        _roomService = roomService;
        _hubContext = hubContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomViewModel>>> Get()
    {
        var rooms = await _roomService.Get();
        return Ok(rooms);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Room>> Get(int id)
    {
        var room = await _roomService.Get(id);
        if (room == null)
            return NotFound();
        return Ok(room);
    }

    [HttpPost]
    public async Task<ActionResult<Room>> Create(RoomViewModel roomViewModel)
    {
        int id = await _roomService.Create(roomViewModel);

        Room room = await _roomService.Get(id);

        await _hubContext.Clients.All.SendAsync("addChatRoom", new { id = room.Id, name = room.Name });

        return CreatedAtAction(nameof(Get), new { id = room.Id }, new { id = room.Id, name = room.Name });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, RoomViewModel roomViewModel)
    {

        var result = await _roomService.Edit(id, roomViewModel);

        if (result > 0)
        {
            Room room = await _roomService.Get(id);

            await _hubContext.Clients.All.SendAsync("updateChatRoom", new { id = room.Id, room.Name });

            return Ok();
        }
        return BadRequest();


    }

    [HttpDelete("{id}/{userName}")]
    public async Task<IActionResult> Delete(int id, string userName)
    {
        int result = await _roomService.Delete(id, userName);

        if (result > 0)
        {

            Room room = await _roomService.Get(id);

            await _hubContext.Clients.All.SendAsync("removeChatRoom", room.Id);
            await _hubContext.Clients.Group(room.Name).SendAsync("onRoomDeleted", string.Format("Room {0} has been deleted.\nYou are moved to the first available room!", room.Name));

            return Ok();
        }
        return BadRequest();
    }
}