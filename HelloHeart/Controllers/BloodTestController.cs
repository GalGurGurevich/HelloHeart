using HelloHeart.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public BloodTestController(ILogger<BloodTestController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<BloodTestResponse> Get()
        {
            BloodTestResponse bloodTestResponse = new BloodTestResponse() { Result = "bloodTestResponse" };
            return Ok(bloodTestResponse);
        }

        [HttpPost, Route("SetResults")]
        public ActionResult<BloodTestResponse> SetResults([FromBody] BloodTestRequest bloodTest)
        {
            var dataSet = _configuration["BloodTestConfig:Url"];
            BloodTestResponse bloodTestResponse = new BloodTestResponse() { Result = "bloodTestResponse" };
            return Ok(bloodTestResponse);
        }

    }
}
