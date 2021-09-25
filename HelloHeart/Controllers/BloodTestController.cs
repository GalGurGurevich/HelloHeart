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
        private readonly IBloodTestManager _bloodTestManager;

        public BloodTestController(ILogger<BloodTestController> logger, IBloodTestManager bloodTestManager)
        {
            _logger = logger;
            _bloodTestManager = bloodTestManager;
        }

        [HttpGet]
        public async Task<ActionResult<BloodTestResponse>> Get()
        {
            var result = await _bloodTestManager.GetBloodTestConfig();
            return Ok(result);
        }

        [HttpPost, Route("SetResults")]
        public async Task<ActionResult<BloodTestResponse>> SetResults([FromBody] BloodTestRequest bloodTest)
        {
            BloodTestResponse bloodTestResponse = await _bloodTestManager.BloodTestAnalysis(bloodTest);
            return Ok(bloodTestResponse);
        }

    }
}
