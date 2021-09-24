using HelloHeart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HelloHeart.Manager
{
    public class BloodTestManager : IBloodTestManager
    {
        static HttpClient client = new HttpClient();

        public async Task<List<BloodTestConfigDataRow>> GetBloodTestConfig(string path)
        {
            var dataSet = new List<BloodTestConfigDataRow>();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                //dataSet = await response.Content.ToString()
            }
            throw new NotImplementedException();
        }
    }
}
