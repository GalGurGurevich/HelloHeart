using HelloHeart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HelloHeart.Helpers
{
    public class InputValidator : IInputValidator
    {
        public string ExtractKey(string input, BloodTestConfigResponse map)
        {
            string key = "";

            if (input.Length < 2)
            {
                return key;
            }

            HashSet<string> set = new HashSet<string>();

            foreach (var item in map.BloodTestConfig)
            {
                set.Add(item.Name);
            }

            if(input.Contains(" "))
            {
                var collection = input.Split(new string[] { " ", "-" }, StringSplitOptions.None);
                key = SearchStringMatchInCollection(collection, map.BloodTestConfig.Select(x => x.Name).ToList());
                if (!string.IsNullOrEmpty(key))
                    return key;
            }
            else
            {
                if (map.BloodTestConfig.Any(x => x.Name.Contains(input)))
                {
                    var options = map.BloodTestConfig.Where(x => x.Name.Contains(input)).ToList();
                    if(options != null)
                    {
                        if(options.Count == 1)
                        {
                            var match = options.FirstOrDefault().Name;
                            key = match;
                            return key;
                        }
                        if(options.Count > 1)
                        {
                            return key;
                        }
                    }
                    //var res = map.BloodTestConfig.FirstOrDefault(x => x.Name.Contains(input));
                    //key = res.Name;
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

        public string SearchStringMatchInCollection(IEnumerable<string> keys, IEnumerable<string> collection)
        {
            foreach (var item in collection)
            {
                foreach (var key in keys)
                {
                    if (string.IsNullOrEmpty(key)) continue;

                    if (item.Contains(CleanStringFromSymbols(key)))
                        return item;
                }
            }
            return "";
        }

        public string CleanStringFromSymbols(string input)
        {
            return input.Trim(new Char[] { ' ', '*', '.', ',', });
        }

    }
}
