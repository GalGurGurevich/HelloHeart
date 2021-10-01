using HelloHeart.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloHeart.Manager
{
    public interface IBloodTestManager
    {
        public Task<Dictionary<string, int>> GetBloodTestConfig();
        public Task<BloodTestResponse> BloodTestAnalysis(BloodTestRequest bloodTest);
    }
}