using HelloHeart.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloHeart.Manager
{
    public interface IBloodTestManager
    {
        public Task<BloodTestConfigResponse> GetBloodTestConfig(string path);
        public string BloodTestAnalysis(BloodTestRequest bloodTest);
    }
}