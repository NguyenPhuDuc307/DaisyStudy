using DaisyStudy.Application.Catalog.Questions;
using DaisyStudy.Data.Entities;
using DaisyStudy.ViewModels.Catalog.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DaisyStudy.BackendApi.Controllers;

// api/questions
[Route("api/[controller]")]
[ApiController]
/*[Authorize]*/
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionsController (IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] QuestionsCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var id = await _questionService.Create(request);
        if (id == 0)
            return BadRequest();

        var question = await _questionService.GetById(id);

        return CreatedAtAction(nameof(GetById), new { id = id }, question);
    }

    [HttpDelete("{questionID}")]
    public async Task<IActionResult> Delete(int questionID)
    {
        var affectedResult = await _questionService.Delete(questionID);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }

    // http://localhost:post/questions/paging?pageIndex=1&pageSize=10
    [HttpGet("paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] GetManageQuestionPagingRequest request)
    {
        var questions = await _questionService.GetAllPaging(request);
        return Ok(questions);
    }

    // http://localhost:post/questions/1
    [HttpGet("{questionId}")]
    public async Task<IActionResult> GetById(int questionId)
    {
        var question = await _questionService.GetById(questionId);
        if (question == null)
            return BadRequest("Cannot find question");
        return Ok(question);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] QuestionUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var affectedResult = await _questionService.Update(request);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }
}


