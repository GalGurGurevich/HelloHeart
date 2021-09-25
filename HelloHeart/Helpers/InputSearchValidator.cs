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

            var matchExists = CheckIfAnyMachExisits(input, data);

            if (!matchExists) return key;

            key = StringMatchAlgo(input, data);
            
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
        public string CleanStringFromSymbols(string input)
        {
            return input.Trim(new Char[] { ' ', '*', '.', ',', '-'});
        }
        private string StringMatchAlgo(string input, BloodTestConfigResponse data)
        {
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();

            var scoredResults = FuzzySharp.Process.ExtractSorted(input, data.BloodTestConfig.Select(x => x.Name));

            var mostMatch = scoredResults?.FirstOrDefault();

            var arr = scoredResults?.ToArray();

            if (arr.Length >= 2)
            {
                if(mostMatch.Score == arr[1].Score)
                {
                    return "";
                }
            }

            return mostMatch.Value;

        }
        private bool CheckIfAnyMachExisits(string input, BloodTestConfigResponse data)
        {
            Regex regex = new Regex(CleanStringFromSymbols(input));
            bool containsAny = false;

            if (input.Contains(" "))
            {
                var allTextWords = input.Split(new string[] { " ", "-" }, StringSplitOptions.None);

                foreach (var test in data.BloodTestConfig)
                {
                    foreach (var word in allTextWords)
                    {
                        if (string.IsNullOrEmpty(word)) continue;

                        Regex reg = new Regex(CleanStringFromSymbols(word));
                        containsAny = reg.IsMatch(test.Name.ToUpper());
                        if (containsAny)
                            return containsAny;
                    }
                }
            }

            foreach (var test in data.BloodTestConfig)
            {
                containsAny = regex.IsMatch(test.Name.ToUpper());
                if (containsAny)
                    return containsAny;
            }

            return containsAny;
             
        }

    }
}
