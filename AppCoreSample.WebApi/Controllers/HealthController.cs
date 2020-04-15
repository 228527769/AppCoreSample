using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppCoreSample.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            //Console.WriteLine($"此服务的ip:{_configuration["ip"]},port:{_configuration["port"]}");

            return Ok();
        }
    }
}