using DaisyStudy.Application.Catalog.Messages;
using DaisyStudy.Application.Catalog.Rooms;
using DaisyStudy.BackendApi.Hubs;
using DaisyStudy.Data.Entities;
using DaisyStudy.ViewModels.Catalog.Messages;
using DaisyStudy.ViewModels.Catalog.Rooms;
using DaisyStudy.ViewModels.Catalog.Uploads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DaisyStudy.BackendApi.Controllers;

// api/messages
//[Authorize]
[Route("api/[controller]")]
[ApiController]

public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly IRoomService _roomService;
    private readonly IHubContext<ChatHub> _hubContext;

    public MessagesController(IMessageService messageService, IRoomService roomService, IHubContext<ChatHub> hubContext)
    {
        _messageService = messageService;
        _roomService = roomService;
        _hubContext = hubContext;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Room>> Get(int id)
    {
        var message = await _messageService.Get(id);
        if (message == null)
            return NotFound();

        return Ok(message);
    }

    [HttpGet("Room/{roomName}")]
    public async Task<ActionResult<IEnumerable<MessageViewModel>>> GetMessages(string roomName)
    {
        var messagesViewModel = await _messageService.GetMessages(roomName);
        return Ok(messagesViewModel);
    }

    [HttpPost]
    public async Task<ActionResult<Message>> Create(MessageViewModel messageViewModel)
    {
        var createdMessage = await _messageService.Create(messageViewModel);
        var room = await _roomService.Get(messageViewModel.Room);
        // Broadcast the message
        await _hubContext.Clients.Group(room.Name).SendAsync("newMessage", createdMessage);

        return CreatedAtAction(nameof(Get), new { id = createdMessage.Id }, createdMessage);
    }

    [HttpDelete("{id}/{userName}")]
    public async Task<IActionResult> Delete(int id, string userName)
    {
        int Id = await _messageService.Delete(id, userName);

        await _hubContext.Clients.All.SendAsync("removeChatMessage", Id);

        return Ok();
    }
}