using DaisyStudy.Application.Catalog.RoomChats;
using DaisyStudy.BackendApi.Hubs;
using DaisyStudy.ViewModels.Catalog.RoomChats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DaisyStudy.BackendApi.Controllers;

// api/RoomChats
[Route("api/[controller]")]
[ApiController]
public class RoomChatsController : ControllerBase
{
    private readonly IRoomChatService _RoomChatService;
    private readonly IHubContext<ChatHub> _hubContext;

    public RoomChatsController(IRoomChatService RoomChatService, IHubContext<ChatHub> hubContext)
    {
        _RoomChatService = RoomChatService;
        _hubContext = hubContext;
    }

    // http://localhost:post/RoomChats/1
    [HttpGet("{roomChatId}")]
    public async Task<IActionResult> GetById(int roomChatId)
    {
        var RoomChat = await _RoomChatService.GetById(roomChatId);
        if (RoomChat == null)
            return BadRequest("Cannot find RoomChat");
        return Ok(RoomChat);
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoomChatViewModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var id = await _RoomChatService.Create(request);
        if (id == 0)
            return BadRequest();

        var RoomChat = await _RoomChatService.GetById(id);

        await _hubContext.Clients.All.SendAsync("addChatRoom", new { id = RoomChat.RoomChatID, name = RoomChat.RoomChatName });

        return CreatedAtAction(nameof(GetById), new { id = id }, new { id = RoomChat.RoomChatID, name = RoomChat.RoomChatName });
    }

    [HttpPut]
    public async Task<IActionResult> Update(int roomChatID, RoomChatViewModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var id = await _RoomChatService.Update(roomChatID, request);
        if (id == 0)
            return BadRequest();
        var RoomChat = await _RoomChatService.GetById(id);
        await _hubContext.Clients.All.SendAsync("updateChatRoom", new { id = RoomChat.RoomChatID, name = RoomChat.RoomChatName });

        return NoContent();
    }

    [HttpDelete("{roomChatId}")]
    public async Task<IActionResult> Delete(int roomChatId)
    {
        var RoomChat = await _RoomChatService.GetById(roomChatId);
        var id = await _RoomChatService.Delete(roomChatId);
        if (id == 0)
            return BadRequest();
        await _hubContext.Clients.All.SendAsync("removeChatRoom", RoomChat.RoomChatID);
        await _hubContext.Clients.Group(RoomChat.RoomChatName).SendAsync("onRoomDeleted", string.Format("Room {0} has been deleted.\nYou are moved to the first available room!", RoomChat.RoomChatName));
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var RoomChats = await _RoomChatService.GetAll();
        return Ok(RoomChats);
    }
}