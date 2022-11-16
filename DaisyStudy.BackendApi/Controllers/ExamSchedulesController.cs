using DaisyStudy.Application.Catalog.ExamSchedules;
using DaisyStudy.ViewModels.Catalog.ExamSchedules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.BackendApi.Controllers;

// api/examschedules
[Route("api/[controller]")]
[ApiController]
/*[Authorize]*/
public class ExamSchedulesController : ControllerBase
{
    private readonly IExamSchedulesService _examscheduleService;

    public ExamSchedulesController(IExamSchedulesService examSchedulesService)
    {
        _examscheduleService = examSchedulesService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ExamSchedulesCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var id = await _examscheduleService.Create(request);
        if (id == 0)
            return BadRequest();

        var examschedule = await _examscheduleService.GetById(id);

        return CreatedAtAction(nameof(GetById), new { id = id }, examschedule);
    }

    [HttpDelete("{examscheduleID}")]
    public async Task<IActionResult> Delete(int examscheduleID)
    {
        var affectedResult = await _examscheduleService.Delete(examscheduleID);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }

    // http://localhost:post/examschedules/paging?pageIndex=1&pageSize=10
    [HttpGet("paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] GetManageExamSchedulesPagingRequest request)
    {
        var examschedules = await _examscheduleService.GetAllPaging(request);
        return Ok(examschedules);
    }

    // http://localhost:post/examschedules/1
    [HttpGet("{examscheduleID}")]
    public async Task<IActionResult> GetById(int examscheduleID)
    {
        var examschedule = await _examscheduleService.GetById(examscheduleID);
        if (examschedule == null)
            return BadRequest("Cannot find examschedule");
        return Ok(examschedule);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] ExamSchedulesUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var affectedResult = await _examscheduleService.Update(request);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }
}