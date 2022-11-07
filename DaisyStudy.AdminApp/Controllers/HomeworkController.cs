using DaisyStudy.ApiIntegration.Catalog.Classes;
using DaisyStudy.ApiIntegration.Catalog.Homeworks;
using DaisyStudy.ViewModels.Catalog.Classes;
using DaisyStudy.ViewModels.Catalog.ClassImages;
using DaisyStudy.ViewModels.Catalog.Homeworks;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.AdminApp.Controllers
{
    public class HomeworkController : BaseController
    {
        private readonly IHomeworkApiClient _homeworkApiClient;
        private readonly IConfiguration _configuration;

        public HomeworkController(IHomeworkApiClient homeworkApiClient,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _homeworkApiClient = homeworkApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetManageHomeworkPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _homeworkApiClient.GetHomeworkPaging(request);
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
        public async Task<IActionResult> Create([FromForm] HomeworkCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _homeworkApiClient.CreateHomework(request);
            if (result != null)
            {
                TempData["result"] = "Thêm mới lớp học thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm lớp học thất bại");
            return View(request);
        }
    }
}