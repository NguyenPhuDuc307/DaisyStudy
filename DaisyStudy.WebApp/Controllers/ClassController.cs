using DaisyStudy.ApiIntegration.Catalog.Classes;
using DaisyStudy.ViewModels.Catalog.Classes;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.WebApp.Controllers;

public class ClassController : BaseController
{
    private readonly IClassApiClient _classApiClient;
    private readonly IConfiguration _configuration;

    public ClassController(IClassApiClient classApiClient,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _classApiClient = classApiClient;
    }

    public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
    {
        var request = new GetManageClassPagingRequest()
        {
            Keyword = keyword,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
        var data = await _classApiClient.GetPublicClassPaging(request);
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
    public async Task<IActionResult> Create([FromForm] ClassCreateRequest request)
    {
        request.UserName = User.Identity.Name;
        if (!ModelState.IsValid)
            return View(request);

        var result = await _classApiClient.CreateClass(request);
        if (result)
        {
            TempData["result"] = "Thêm mới lớp học thành công";
            return RedirectToAction("Index");
        }

        ModelState.AddModelError("", "Thêm lớp học thất bại");
        return View(request);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImage()
    {
        string filePath = "";
        if (!ModelState.IsValid)
            return View();
        List<ClassImageUpdateRequest> list = new List<ClassImageUpdateRequest>();
        foreach (IFormFile image in Request.Form.Files)
        {
            ClassImageUpdateRequest classImageUpdateRequest = new ClassImageUpdateRequest();
            classImageUpdateRequest.ThumbnailImage = image;
            list.Add(classImageUpdateRequest);
        }

        foreach (ClassImageUpdateRequest classImageCreateRequest in list)
        {
            filePath = _configuration["BaseAddress"] + await _classApiClient.UploadImage(classImageCreateRequest);
        }
        return Json(new { url = filePath });
    }

    [HttpGet]
    public async Task<IActionResult> OverView(int ClassID)
    {
        var result = await _classApiClient.GetById(ClassID);
        if (result.ResultObj.TeacherImage == "https://localhost:5001/")
            result.ResultObj.TeacherImage = null;
        return View(result.ResultObj);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int ClassID)
    {
        var result = await _classApiClient.GetById(ClassID);
        return View(result.ResultObj);
    }
}