using DaisyStudy.Application.Catalog.Messages;
using DaisyStudy.Application.Catalog.Rooms;
using DaisyStudy.Application.Catalog.Uploads;
using DaisyStudy.BackendApi.Hubs;
using DaisyStudy.Data.Entities;
using DaisyStudy.ViewModels.Catalog.Uploads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DaisyStudy.BackendApi.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UploadsController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IRoomService _roomService;
    private readonly IUploadImageService _uploadService;

    public UploadsController(IHubContext<ChatHub> hubContext, IRoomService roomService, IUploadImageService uploadService)
    {
        _hubContext = hubContext;
        _roomService = roomService;
        _uploadService = uploadService;
    }

    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<ActionResult<Message>> Upload([FromForm] UploadViewModel uploadViewModel)
    {
        if (ModelState.IsValid)
        {

            var createdMessage = await _uploadService.Upload(uploadViewModel);
            var room = await _roomService.Get(uploadViewModel.RoomId);
            // Broadcast the message
            await _hubContext.Clients.Group(room.Name).SendAsync("newMessage", createdMessage);

            return Ok();
        }

        return BadRequest();
    }
}
