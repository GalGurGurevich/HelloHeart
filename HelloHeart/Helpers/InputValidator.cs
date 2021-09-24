using HelloHeart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HelloHeart.Helpers
{
    public class InputValidator
    {
        public string ExtractKey(string input, BloodTestConfigResponse map)
        {
            string key = "";
            HashSet<string> set = new HashSet<string>();

            foreach (var item in map.BloodTestConfig)
            {
                set.Add(item.Name);
            }

            if (map.BloodTestConfig.Any(x => x.Name.Contains(input)))
            {
                var res = map.BloodTestConfig.FirstOrDefault(x => x.Name.Contains(input));
                key = res.Name;
            }
            return key;
        }
    }
}
