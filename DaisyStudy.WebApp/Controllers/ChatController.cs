using DaisyStudy.ApiIntegration.Catalog.Chats;
using DaisyStudy.ViewModels.Catalog.Chats;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.WebApp.Controllers;

public class ChatController : BaseController
{
    private readonly IChatApiClient _chatApiClient;
    private readonly IConfiguration _configuration;

    public ChatController(IChatApiClient chatApiClient,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _chatApiClient = chatApiClient;
    }

    public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
    {
        var request = new GetManageChatPagingRequest()
        {
            Keyword = keyword,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
        var data = await _chatApiClient.GetChatPaging(request);
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
        if (User.Identity != null)
        {
            var user = User.Identity.Name;
        }
        return View();
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] ChatCreateRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);
            

        var result = await _chatApiClient.CreateChat(request);
        if (result)
        {
            TempData["result"] = "Thêm mới thông báo thành công";
            return RedirectToAction("Index");
        }

        ModelState.AddModelError("", "Thêm thông báo thất bại");
        return View(request);
    }
}