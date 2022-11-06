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

        // http://localhost:post/classes/paging?pageIndex=1&pageSize=10
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllClassPaging([FromQuery] GetManageClassPagingRequest request)
        {
            var classes = await _classService.GetAllClassPaging(request);
            return Ok(classes);
        }

        // http://localhost:post/classes/paging?pageIndex=1&pageSize=10
        [HttpGet("student/paging")]
        public async Task<IActionResult> GetAllStudentByClassIDPaging([FromQuery] GetAllStudentInClassPagingRequest request)
        {
            var students = await _classService.GetAllStudentByClassIDPaging(request);
            return Ok(students);
        }

        // http://localhost:post/classes/1
        [HttpGet("{classId}")]
        public async Task<IActionResult> GetById(int classId)
        {
            var _class = await _classService.GetById(classId);
            if (_class == null)
                return BadRequest("Cannot find class");
            return Ok(_class);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
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

        [HttpPost("addStudent")]
        public async Task<IActionResult> AddStudent(string ClassID, string UserName)
        {
            if (ClassID == null || UserName == null)
            {
                return BadRequest();
            }
            var affectedResult = await _classService.AddStudent(ClassID, UserName);

            if (affectedResult != true)
                return BadRequest();
            return Ok();
        }

        [HttpPost("uploadImage")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage([FromForm] ClassImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string result = await _classService.UploadImage(request);
            if (result != null)
                return Ok(result);

            return BadRequest();
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
                return BadRequest("Cannot find class");
            return Ok(image);
        }
    }
}