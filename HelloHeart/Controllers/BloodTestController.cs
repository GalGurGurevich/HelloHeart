using HelloHeart.Manager;
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
        private readonly IBloodTestManager _bloodTestManager;

        public BloodTestController(ILogger<BloodTestController> logger, IConfiguration configuration, IBloodTestManager bloodTestManager)
        {
            _logger = logger;
            _configuration = configuration;
            _bloodTestManager = bloodTestManager;
        }

        [HttpGet]
        public async Task<ActionResult<BloodTestResponse>> Get()
        {
            var path = _configuration["BloodTestConfig:Url"];
            var result = await _bloodTestManager.GetBloodTestConfig(path);
            return Ok(result);
        }

        [HttpPost, Route("SetResults")]
        public ActionResult<BloodTestResponse> SetResults([FromBody] BloodTestRequest bloodTest)
        {
            BloodTestResponse bloodTestResponse = new BloodTestResponse();
            bloodTestResponse.Result = _bloodTestManager.BloodTestAnalysis(bloodTest);
            return Ok(bloodTestResponse);
        }

    }
}
