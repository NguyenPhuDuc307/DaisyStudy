using DaisyStudy.Application.Catalog.StudentExams;
using DaisyStudy.ViewModels.Catalog.StudentExams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.BackendApi.Controllers;

// api/studentexams
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentExamController : ControllerBase
{
    private readonly IStudentExamService _studentexamService;

    public StudentExamController(IStudentExamService studentexamService)
    {
        _studentexamService = studentexamService;
    }

    // http://localhost:post/studentexams/1
    [HttpGet("{studentexamId}")]
    public async Task<IActionResult> GetById(int studentexamId)
    {
        var studentexam = await _studentexamService.GetById(studentexamId);
        if (studentexam == null)
            return BadRequest("Cannot find studentexam");
        return Ok(studentexam);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] StudentExamsCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var id = await _studentexamService.Create(request);
        if (id == 0)
            return BadRequest();

        var studentexam = await _studentexamService.GetById(id);

        return CreatedAtAction(nameof(GetById), new { id = id }, studentexam);
    }

    [HttpPut]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update([FromForm] StudentExamsUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var affectedResult = await _studentexamService.Update(request);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }

    [HttpDelete("{studentexamId}")]
    public async Task<IActionResult> Delete(int studentexamId)
    {
        var affectedResult = await _studentexamService.Delete(studentexamId);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }

    // http://localhost:post/studentexams/paging?pageIndex=1&pageSize=10
    [HttpGet("paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] GetManageStudentExamPagingRequest request)
    {
        var studentexam = await _studentexamService.GetAllPaging(request);
        return Ok(studentexam);
    }
}