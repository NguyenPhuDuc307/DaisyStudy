using DaisyStudy.Application.Catalog.Chats;
using DaisyStudy.ViewModels.Catalog.Chats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.BackendApi.Controllers;

// api/chats
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ChatsController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatsController(IChatService chatService)
    {
        _chatService = chatService;
    }

    // http://localhost:post/chats/1
    [HttpGet("{chatId}")]
    public async Task<IActionResult> GetById(int chatId)
    {
        var chat = await _chatService.GetById(chatId);
        if (chat == null)
            return BadRequest("Cannot find Chat");
        return Ok(chat);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] ChatCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var id = await _chatService.Create(request);
        if (id == 0)
            return BadRequest();

        var chat = await _chatService.GetById(id);

        return CreatedAtAction(nameof(GetById), new { id = id }, chat);
    }

    [HttpDelete("{chatId}")]
    public async Task<IActionResult> Delete(int ChatId)
    {
        var affectedResult = await _chatService.Delete(ChatId);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }

    // http://localhost:post/chats/paging?pageIndex=1&pageSize=10
    [HttpGet("paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] GetManageChatPagingRequest request)
    {
        var chats = await _chatService.GetAllPaging(request);
        return Ok(chats);
    }
}