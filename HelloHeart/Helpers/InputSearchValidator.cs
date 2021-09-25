using HelloHeart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HelloHeart.Helpers
{
    public class InputSearchValidator : IInputSearchValidator
    {
        public string DiagnoseBloodTest(string input, BloodTestConfigResponse data)
        {
            string key = "";

            if (input.Length < 2)
            {
                return key;
            }

            if(input.Contains(" "))
            {
                var collection = input.Split(new string[] { " ", "-" }, StringSplitOptions.None);
                key = SearchStringMatchInCollection(collection, data.BloodTestConfig.Select(x => x.Name).ToList());
                if (!string.IsNullOrEmpty(key))
                    return key;
            }
            else
            {
                var cleanInput = CleanStringFromSymbols(input);

                if (data.BloodTestConfig.Any(x => x.Name.Contains(cleanInput)))
                {
                    var options = data.BloodTestConfig.Where(x => x.Name.Contains(cleanInput)).ToList();
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
                }
            }

            return key;
        }

        public DiagnoseStatus DiagnoseCondition(string key, BloodTestConfigResponse data, int input)
        {
            var pair = data.BloodTestConfig.FirstOrDefault(x => x.Name == key);
            if (pair == null)
                return DiagnoseStatus.Unknown;

            if(input <= pair.Threshold)
            {
                return DiagnoseStatus.Good;
            } 
            else if(input > pair.Threshold)
            {
                return DiagnoseStatus.Bad;
            }
            else
            {
                return DiagnoseStatus.Unknown;
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
