using HelloHeart.Helpers;
using HelloHeart.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HelloHeart.Manager
{
    public class BloodTestManager : IBloodTestManager
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IInputSearchValidator _inputValidator;
        private readonly IMemoryCache _cache;

        public BloodTestManager(IConfiguration configuration, IHttpClientFactory clientFactory, IInputSearchValidator inputValidator, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            _inputValidator = inputValidator;
            _cache = memoryCache;
        }
        public async Task<BloodTestResponse> BloodTestAnalysis(BloodTestRequest bloodTest)
        {
            BloodTestResponse bloodTestResponse = new BloodTestResponse();
            var bloodTestConfigMap = await GetBloodTestConfig();

            bloodTestResponse.Result = _inputValidator.DiagnoseBloodTest(bloodTest.TestInput, bloodTestConfigMap);
            bloodTestResponse.ResultEvaluation = _inputValidator.DiagnoseCondition(bloodTestResponse.Result, bloodTestConfigMap, int.Parse(bloodTest.TestNumber));

            return bloodTestResponse;
        }

        public async Task<BloodTestConfigResponse> GetBloodTestConfig()
        {
            var path = _configuration["BloodTestConfig:Url"];
            var client = _clientFactory.CreateClient();
            BloodTestConfigResponse bloodTestConfigResponse = new BloodTestConfigResponse();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var bloodTestObj = await response.Content.ReadAsStringAsync();
                var dataSet = JsonConvert.DeserializeObject<BloodTestConfigResponse>(bloodTestObj);
                if (dataSet?.BloodTestConfig != null)
                    return dataSet;
            }

            return bloodTestConfigResponse;
        }
    }
}
