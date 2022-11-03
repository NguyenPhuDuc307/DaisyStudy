using Microsoft.AspNetCore.Mvc;
using DaisyStudy.ViewModels.System.Users;
using DaisyStudy.AdminApp.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DaisyStudy.AdminApp.Controllers;

public class UserController : BaseController
{
    private readonly IUserApiClient _userApiClient;
    private readonly IConfiguration _configuration;

    public UserController(IUserApiClient userApiClient, IConfiguration configuration)
    {
        _userApiClient = userApiClient;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
    {
        var request = new GetUserPagingRequest()
        {
            Keyword = keyword,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
        var data = await _userApiClient.GetUsersPaging(request);
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
    public async Task<IActionResult> Create(RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return View();

        var result = await _userApiClient.RegisterUser(request);
        if (result.IsSuccessed)
        {
            TempData["result"] = "Thêm mới người dùng thành công";
            return RedirectToAction("Index");
        }

        ModelState.AddModelError("", result.Message);
        return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await _userApiClient.GetById(id);
        if (result.IsSuccessed)
        {
            var user = result.ResultObj;
            if (user != null)
            {
                var updateRequest = new UserUpdateRequest()
                {
                    Dob = user.Dob,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Id = id
                };
                return View(updateRequest);
            }
        }
        return RedirectToAction("Error", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UserUpdateRequest request)
    {
        if (!ModelState.IsValid)
            return View();

        var result = await _userApiClient.UpdateUser(request.Id, request);
        if (result.IsSuccessed)
        {
            TempData["result"] = "Cập nhật người dùng thành công";
            return RedirectToAction("Index");
        }
        ModelState.AddModelError("", result.Message);
        return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _userApiClient.GetById(id);
        if (result.IsSuccessed)
        {
            var user = result.ResultObj;
            if (user != null)
            {
                var deleteRequest = new UserDeleteRequest()
                {
                    Dob = user.Dob,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Id = id
                };
                return View(deleteRequest);
            }
        }
        return RedirectToAction("Error", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(UserDeleteRequest request)
    {
        if (!ModelState.IsValid)
            return View();

        var result = await _userApiClient.Delete(request.Id);
        if (result.IsSuccessed)
        {
            TempData["result"] = "Xoá người dùng thành công";
            return RedirectToAction("Index");
        }

        ModelState.AddModelError("", result.Message);
        return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _userApiClient.GetById(id);
        return View(result.ResultObj);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Remove("Token");
        return RedirectToAction("Index", "Login");
    }
}