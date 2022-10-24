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
        //private readonly IPublicClassService _publicClassService;
        //private readonly IManageClassService _manageClassService;

        //public ClassController(IPublicClassService publicClassService,
        //    IManageClassService manageClassService)
        //{
        //    _publicClassService = publicClassService;
        //    _manageClassService = manageClassService;
        //}

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Test Ok");
        }
    }
}