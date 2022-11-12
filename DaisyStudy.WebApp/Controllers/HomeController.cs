using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DaisyStudy.WebApp.Models;
using DaisyStudy.ViewModels.Catalog.Classes;
using DaisyStudy.ApiIntegration.Catalog.Classes;

namespace DaisyStudy.WebApp.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IClassApiClient _classApiClient;

    public HomeController(ILogger<HomeController> logger, IClassApiClient classApiClient)
    {
        _logger = logger;
        _classApiClient = classApiClient;
    }

    public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
    {
        if (User.Identity != null)
        {
            var user = User.Identity.Name;
        }
        
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

    public IActionResult Chat()
    {
        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

