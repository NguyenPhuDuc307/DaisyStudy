using DaisyStudy.ApiIntegration.Catalog.Notifications;
using DaisyStudy.ViewModels.Catalog.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.WebApp.Controllers;

public class NotificationController : BaseController
{
    private readonly INotificationApiClient _notificationApiClient;
    private readonly IConfiguration _configuration;

    public NotificationController(INotificationApiClient notificationApiClient,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _notificationApiClient = notificationApiClient;
    }

    public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
    {
        var request = new GetManageNotificationPagingRequest()
        {
            Keyword = keyword,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
        var data = await _notificationApiClient.GetNotificationPaging(request);
        ViewBag.Keyword = keyword;
        if (TempData["result"] != null)
        {
            ViewBag.SuccessMsg = TempData["result"];
        }
        return View(data.ResultObj);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] NotificationCreateRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);
            

        var result = await _notificationApiClient.CreateNotification(request);
        if (result)
        {
            TempData["result"] = "Thêm mới thông báo thành công";
            return RedirectToAction("Index");
        }

        ModelState.AddModelError("", "Thêm thông báo thất bại");
        return View(request);
    }
}