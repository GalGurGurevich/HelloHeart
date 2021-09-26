using HelloHeart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HelloHeart.Helpers
{
    public class UserBloodTestAnalyzer : IUserBloodTestAnalyzer
    {
        public string DiagnoseBloodTest(string input, BloodTestConfigResponse data)
        {
            string bloodTestKey = StringMatchAlgo(input, data);
            return bloodTestKey;
        }
        public DiagnoseStatus DiagnoseCondition(string bloodTestKey, BloodTestConfigResponse data, int input)
        {
            var bloodTestEntity = data.BloodTestConfig.FirstOrDefault(x => x.Name == bloodTestKey);
            if (bloodTestEntity == null)
                return DiagnoseStatus.Unknown;

            if(input <= bloodTestEntity.Threshold)
            {
                return DiagnoseStatus.Good;
            } 
            else if(input > bloodTestEntity.Threshold)
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
            if (input.Length < 2)
            {
                return "";
            }

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
