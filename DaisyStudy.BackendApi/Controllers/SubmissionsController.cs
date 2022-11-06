using DaisyStudy.Application.Catalog.Homeworks;
using DaisyStudy.Application.Catalog.Submissions;
using DaisyStudy.ViewModels.Catalog.Homeworks;
using DaisyStudy.ViewModels.Catalog.Submissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.BackendApi.Controllers;

// api/submissions
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SubmissionsController : ControllerBase
{
    private readonly ISubmissionService _submissionService;

    public SubmissionsController(ISubmissionService submissionService)
    {
        _submissionService = submissionService;
    }

    // http://localhost:post/submissions/1
    [HttpGet("{homeworkID}/{userName}")]
    public async Task<IActionResult> GetById(int homeworkID, string userName)
    {
        var submission = await _submissionService.GetById(homeworkID, userName);
        if (submission == null)
            return BadRequest("Cannot find submission");
        return Ok(submission);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] SubmissionCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _submissionService.Create(request);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] SubmissionUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _submissionService.Update(request);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPut("mark")]
    public async Task<IActionResult> Update([FromForm] SubmissionUpdateMarkRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _submissionService.UpdateMark(request);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpDelete("{homeworkID}/{userName}")]
    public async Task<IActionResult> Delete(int homeworkID, string userName)
    {
        var result = await _submissionService.Delete(homeworkID, userName);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    // http://localhost:post/submissions/paging?pageIndex=1&pageSize=10
    [HttpGet("paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] GetManageSubmissionPagingRequest request)
    {
        var products = await _submissionService.GetAllPaging(request);
        return Ok(products);
    }
}