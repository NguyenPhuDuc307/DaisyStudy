using DaisyStudy.Application.Catalog.Notifications;
using DaisyStudy.ViewModels.Catalog.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.BackendApi.Controllers;

// api/notifications
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    // http://localhost:post/notifications/1
    [HttpGet("{notificationId}")]
    public async Task<IActionResult> GetById(int notificationId)
    {
        var notification = await _notificationService.GetById(notificationId);
        if (notification == null)
            return BadRequest("Cannot find notification");
        return Ok(notification);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] NotificationCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var id = await _notificationService.Create(request);
        if (id == 0)
            return BadRequest();

        var notification = await _notificationService.GetById(id);

        return CreatedAtAction(nameof(GetById), new { id = id }, notification);
    }

    [HttpPut]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update([FromForm] NotificationUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var affectedResult = await _notificationService.Update(request);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }

    [HttpDelete("{notificationId}")]
    public async Task<IActionResult> Delete(int notificationId)
    {
        var affectedResult = await _notificationService.Delete(notificationId);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }

    // http://localhost:post/notifications/paging?pageIndex=1&pageSize=10
    [HttpGet("paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery]GetManageNotificationPagingRequest request)
    {
        var notifications = await _notificationService.GetAllPaging(request);
        return Ok(notifications);
    }
}