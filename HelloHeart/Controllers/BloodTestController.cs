using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloHeart.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BloodTestController : ControllerBase
    {
        private readonly ILogger<BloodTestController> _logger;

        public BloodTestController(ILogger<BloodTestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<BloodTestResponse> Get()
        {
            BloodTestResponse bloodTestResponse = new BloodTestResponse() { Result = "bloodTestResponse" };
            return Ok(bloodTestResponse);
        }
    }
}
