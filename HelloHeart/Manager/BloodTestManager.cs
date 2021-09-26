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
        private readonly IUserBloodTestAnalyzer _inputValidator;
        private readonly IMemoryCache _cache;
        private static string _key = "key";

        public BloodTestManager(IConfiguration configuration, IHttpClientFactory clientFactory, IUserBloodTestAnalyzer inputValidator, IMemoryCache memoryCache)
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
            BloodTestConfigResponse bloodTestConfigResponse = new BloodTestConfigResponse();
            if(_cache.TryGetValue(_key, out bloodTestConfigResponse))
            {
                bloodTestConfigResponse = (BloodTestConfigResponse)_cache.Get(_key);
                return bloodTestConfigResponse;
            }
            var path = _configuration["BloodTestConfig:Url"];
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var bloodTestObj = await response.Content.ReadAsStringAsync();
                var dataSet = JsonConvert.DeserializeObject<BloodTestConfigResponse>(bloodTestObj);
                _cache.Set(_key, dataSet);
                if (dataSet?.BloodTestConfig != null)
                    return dataSet;
            }

            return bloodTestConfigResponse;
        }
    }
}
