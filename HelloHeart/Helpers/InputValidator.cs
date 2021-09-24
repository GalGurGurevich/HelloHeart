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

            if(input.Contains(" "))
            {
                var collection = input.Split(" ");
                key = SearchStringMatchInCollection(collection, map.BloodTestConfig.Select(x => x.Name).ToList());
                if (!string.IsNullOrEmpty(key))
                    return key;
            }
            else
            {
                if (map.BloodTestConfig.Any(x => x.Name.Contains(input)))
                {
                    var res = map.BloodTestConfig.FirstOrDefault(x => x.Name.Contains(input));
                    key = res.Name;
                }
            }

            return key;
        }

        public TestValueOutput ExtractValue(string key, BloodTestConfigResponse map, int input)
        {
            var pair = map.BloodTestConfig.FirstOrDefault(x => x.Name == key);
            if (pair == null)
                return TestValueOutput.Unknown;

            if(input <= pair.Threshold)
            {
                return TestValueOutput.Good;
            } 
            else if(input > pair.Threshold)
            {
                return TestValueOutput.Bad;
            }
            else
            {
                return TestValueOutput.Unknown;
            }
        }

        private string SearchStringMatchInCollection(IEnumerable<string> keys, IEnumerable<string> collection)
        {
            foreach (var item in collection)
            {
                foreach (var key in keys)
                {
                    if (item.Contains(key))
                        return item;
                }
            }

            return "";
        }

    }
}
