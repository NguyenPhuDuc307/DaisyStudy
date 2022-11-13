using DaisyStudy.Application.Catalog.Answers;
using DaisyStudy.ViewModels.Catalog.Answers;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.BackendApi.Controllers;

// api/questions
[Route("api/[controller]")]
[ApiController]
/*[Authorize]*/
public class AnswersController : ControllerBase
{
    private readonly IAnswerService _answerService;

    public AnswersController(IAnswerService answerService)
    {
        _answerService = answerService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AnswerCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var id = await _answerService.Create(request);
        if (id == 0)
            return BadRequest();

        var answer = await _answerService.GetById(id);

        return CreatedAtAction(nameof(GetById), new { id = id }, answer);
    }

    [HttpDelete("{answerID}")]
    public async Task<IActionResult> Delete(int answerID)
    {
        var affectedResult = await _answerService.Delete(answerID);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }

    // http://localhost:post/answers/paging?pageIndex=1&pageSize=10
    [HttpGet("paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] GetManageAnswerPagingRequest request)
    {
        var answers = await _answerService.GetAllPaging(request);
        return Ok(answers);
    }

    // http://localhost:post/answers/1
    [HttpGet("{answerID}")]
    public async Task<IActionResult> GetById(int answerId)
    {
        var answer = await _answerService.GetById(answerId);
        if (answer == null)
            return BadRequest("Cannot find answer");
        return Ok(answer);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] AnswerUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var affectedResult = await _answerService.Update(request);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }
}
