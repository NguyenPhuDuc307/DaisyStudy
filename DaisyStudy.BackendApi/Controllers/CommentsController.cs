using DaisyStudy.Application.Catalog.Comments;
using DaisyStudy.ViewModels.Catalog.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.BackendApi.Controllers;

// api/comments
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    // http://localhost:post/comments/1
    [HttpGet("{commentId}")]
    public async Task<IActionResult> GetById(int commentId)
    {
        var comment = await _commentService.GetById(commentId);
        if (comment == null)
            return BadRequest("Cannot find comment");
        return Ok(comment);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CommentCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var id = await _commentService.Create(request);
        if (id == 0)
            return BadRequest();

        var Comment = await _commentService.GetById(id);

        return CreatedAtAction(nameof(GetById), new { id = id }, Comment);
    }

    [HttpPut]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update([FromForm] CommentUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var affectedResult = await _commentService.Update(request);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }

    [HttpDelete("{commentId}")]
    public async Task<IActionResult> Delete(int commentId)
    {
        var affectedResult = await _commentService.Delete(commentId);
        if (affectedResult == 0)
            return BadRequest();
        return Ok();
    }

    // http://localhost:post/comments/paging?pageIndex=1&pageSize=10
    [HttpGet("paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] GetManageCommentPagingRequest request)
    {
        var comments = await _commentService.GetAllPaging(request);
        return Ok(comments);
    }
}