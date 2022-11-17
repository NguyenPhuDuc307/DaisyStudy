using DaisyStudy.ApiIntegration.Catalog.Comments;
using DaisyStudy.ViewModels.Catalog.Comments;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.WebApp.Controllers;

public class CommentController : BaseController
{
    private readonly ICommentApiClient _commentApiClient;
    private readonly IConfiguration _configuration;

    public CommentController(ICommentApiClient commentApiClient,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _commentApiClient = commentApiClient;
    }

    public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
    {
        var request = new GetManageCommentPagingRequest()
        {
            Keyword = keyword,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
        var data = await _commentApiClient.GetCommentPaging(request);
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
    public async Task<IActionResult> Create([FromForm] CommentCreateRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);


        var result = await _commentApiClient.CreateComment(request);
        if (result)
        {
            TempData["result"] = "Bạn vừa bình luận vào bài viết";
            if (request.ReturnUrl != null)
                return Redirect(request.ReturnUrl);
            return RedirectToAction("Index");
        }

        ModelState.AddModelError("", "Bình luận thất bại");
        return View(request);
    }
}