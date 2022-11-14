using DaisyStudy.Application.Catalog.Messages;
using DaisyStudy.Application.Catalog.RoomChats;
using DaisyStudy.BackendApi.Hubs;
using DaisyStudy.ViewModels.Catalog.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DaisyStudy.BackendApi.Controllers;

// api/Messages
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _MessageService;

    private readonly IRoomChatService _roomChatService;
    private readonly IHubContext<ChatHub> _hubContext;

    public MessagesController(IMessageService MessageService, IHubContext<ChatHub> hubContext, IRoomChatService roomChatService)
    {
        _MessageService = MessageService;
        _hubContext = hubContext;
        _roomChatService = roomChatService;
    }

    // http://localhost:post/Messages/1
    [HttpGet("{MessageID}")]
    public async Task<IActionResult> GetById(int MessageID)
    {
        var Message = await _MessageService.GetById(MessageID);
        if (Message == null)
            return BadRequest("Cannot find Message");
        return Ok(Message);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] MessageViewModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var id = await _MessageService.Create(request);

        var room = await _roomChatService.GetByName(request.RoomChatName);
        if (id == 0)
            return BadRequest();

        var Message = await _MessageService.GetById(id);

        await _hubContext.Clients.Group(room.RoomChatName).SendAsync("newMessage", Message);

        return CreatedAtAction(nameof(GetById), new { id = id }, Message);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UpLoadFile([FromForm] UploadViewModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var id = await _MessageService.UploadFile(request);
        if (id == 0)
            return BadRequest();

        var Message = await _MessageService.GetById(id);

        return CreatedAtAction(nameof(GetById), new { id = id }, Message);
    }

    [HttpDelete("{MessageId}")]
    public async Task<IActionResult> Delete(int MessageId)
    {
        var affectedResult = await _MessageService.Delete(MessageId);
        if (affectedResult == 0)
            return BadRequest();
        return NoContent();
    }

    [HttpGet("roomName/{roomName}")]
    public async Task<IActionResult> GetAll(string roomName)
    {
        var Messages = await _MessageService.GetAll(roomName);
        return Ok(Messages);
    }
}