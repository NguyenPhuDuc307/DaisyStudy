using DaisyStudy.ViewModel.Catalog.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DaisyStudy.BackendApi.Controllers
{
    //api/products
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ClassController : ControllerBase
    {
        private readonly IPublicClassService _publicClassService;
        
        public ClassController(IPublicClassService publicClassService)
        {
           _publicClassService = publicClassService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var classes = await _publicClassService.GetAllClass();
            return Ok(classes);
        }
    }
}