using DaisyStudy.AdminApp.Service;
using DaisyStudy.ViewModels.Catalog.Classes;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.AdminApp.Controllers
{
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
            var data = await _classApiClient.GetClassPaging(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
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
    }
}