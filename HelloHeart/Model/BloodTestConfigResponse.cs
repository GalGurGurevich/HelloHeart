using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloHeart.Model
{
    public class BloodTestConfigResponse
    {
        public List<BloodTestConfigEntity> BloodTestConfig { get; set; }
    }

    public class BloodTestConfigEntity
    {
        public string Name { get; set; }
        public int Threshold { get; set; }
    }
}
