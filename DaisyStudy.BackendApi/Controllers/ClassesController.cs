using DaisyStudy.Application.Catalog.Classes;
using DaisyStudy.Data.Entities;
using DaisyStudy.ViewModels.Catalog.Classes;
using DaisyStudy.ViewModels.Catalog.ClassImages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.BackendApi.Controllers
{
    // api/classes
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        // http://localhost:post/classes?pageIndex=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetPublicClassPagingRequest request)
        {
            var classes = await _classService.GetAll(request);
            return Ok(classes);
        }

        // http://localhost:post/classes/1
        [HttpGet("{classId}")]
        public async Task<IActionResult> GetById(int classId)
        {
            var classes = await _classService.GetById(classId);
            if (classes == null)
                return BadRequest("Cannot find class");
            return Ok(classes);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ClassCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _classService.Create(request);
            if (id == 0)
                return BadRequest();

            var _class = await _classService.GetById(id);

            return CreatedAtAction(nameof(GetById), new { id = id }, _class);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ClassUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _classService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{classId}")]
        public async Task<IActionResult> Delete(int classId)
        {
            var affectedResult = await _classService.Delete(classId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPatch("tuition/{classId}/{newTuition}")]
        public async Task<IActionResult> UpdateTuition(int classId, decimal newTuition)
        {
            var isSuccessful = await _classService.UpdateTuition(classId, newTuition);
            if (isSuccessful)
                return Ok();
            return BadRequest();
        }

        [HttpPatch("status/{classId}/{newStatus}")]
        public async Task<IActionResult> UpdateStatus(int classId, Status newStatus)
        {
            var isSuccessful = await _classService.UpdateStatus(classId, newStatus);
            if (isSuccessful)
                return Ok();
            return BadRequest();
        }

        [HttpPatch("public/{classId}/{isPublic}")]
        public async Task<IActionResult> UpdateIsPublic(int classId, IsPublic isPublic)
        {
            var isSuccessful = await _classService.UpdateIsPublic(classId, isPublic);
            if (isSuccessful)
                return Ok();
            return BadRequest();
        }

        [HttpPatch("change-class-id/{id}")]
        public async Task<IActionResult> ChangeClassID(int id)
        {
            var isSuccessful = await _classService.ChangeClassID(id);
            if (isSuccessful)
                return Ok();
            return BadRequest();
        }

        //Images
        [HttpPost("images")]
        public async Task<IActionResult> CreateImage(int classId, [FromForm] ClassImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _classService.AddImage(classId, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _classService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut("images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ClassImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _classService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _classService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpGet("images/{imageId}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var image = await _classService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Cannot find product");
            return Ok(image);
        }
    }
}