using DaisyStudy.Application.Catalog.Homeworks;
using DaisyStudy.ViewModels.Catalog.Homeworks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.BackendApi.Controllers;

// api/homeworks
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class HomeworksController : ControllerBase
{
    private readonly IHomeworkService _homeworkService;

    public HomeworksController(IHomeworkService homeworkService)
    {
        _homeworkService = homeworkService;
    }

    // http://localhost:post/homeworks/1
    [HttpGet("{homeworkId}")]
    public async Task<IActionResult> GetById(int homeworkId)
    {
        var homework = await _homeworkService.GetById(homeworkId);
        if (homework == null)
            return BadRequest("Cannot find homework");
        return Ok(homework);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] HomeworkCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var id = await _homeworkService.Create(request);
        if (id == 0)
            return BadRequest();

        var homework = await _homeworkService.GetById(id);

        return CreatedAtAction(nameof(GetById), new { id = id }, homework);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] HomeworkUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var affectedResult = await _homeworkService.Update(request);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }

    [HttpDelete("{homeworkId}")]
    public async Task<IActionResult> Delete(int homeworkId)
    {
        var affectedResult = await _homeworkService.Delete(homeworkId);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }

    // http://localhost:post/homeworks/paging?pageIndex=1&pageSize=10
    [HttpGet("paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery]GetManageHomeworkPagingRequest request)
    {
        var homeworks = await _homeworkService.GetAllPaging(request);
        return Ok(homeworks);
    }
}