using DaisyStudy.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.Controllers.Components;

public class PagerViewComponent : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
    {
        return Task.FromResult((IViewComponentResult)View("Default", result));
    }
}