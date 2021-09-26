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
        private string StringMatchAlgo(string input, BloodTestConfigResponse data)
        {
            const int minimumMatchLimit = 30;

            var scoredResults = FuzzySharp.Process.ExtractSorted(input, data.BloodTestConfig.Select(x => x.Name));

            var scoredResultsArr = scoredResults?.ToArray();

            if (scoredResultsArr.All(x => x.Score <= minimumMatchLimit))
                return "";

            var mostMatch = scoredResultsArr?.FirstOrDefault();

            if (scoredResultsArr.Length >= 2)
            {
                if(mostMatch.Score == scoredResultsArr[1].Score)
                {
                    return "";
                }
            }

            return mostMatch.Value;

        }

    }
}
