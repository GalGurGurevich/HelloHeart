using HelloHeart.Helpers;
using HelloHeart.Model;
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

        public BloodTestManager(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
        }
        public async Task<BloodTestResponse> BloodTestAnalysis(BloodTestRequest bloodTest)
        {
            BloodTestResponse bloodTestResponse = new BloodTestResponse();
            var path = _configuration["BloodTestConfig:Url"];
            var bloodTestConfigMap = await GetBloodTestConfig(path);

            InputValidator inputValidator = new InputValidator();
            bloodTestResponse.Result = inputValidator.ExtractKey(bloodTest.TestInput, bloodTestConfigMap);
            bloodTestResponse.ResultEvaluation = inputValidator.ExtractValue(bloodTestResponse.Result, bloodTestConfigMap, int.Parse(bloodTest.TestNumber));

            return bloodTestResponse;
        }

        public async Task<BloodTestConfigResponse> GetBloodTestConfig(string path)
        {
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
