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
        public string DiagnoseBloodTest(string input, Dictionary<string, int> data)
        {
            return StringMatchAlgo(input, data);
        }
        public DiagnoseStatus DiagnoseCondition(int input, int treshold)
        {
            if(input <= treshold)
            {
                return DiagnoseStatus.Good;
            } 
            else
            {
                return DiagnoseStatus.Bad;
            }
        }
        private string StringMatchAlgo(string input, Dictionary<string, int> data)
        {
            if (input.Length < 2)
            {
                return "";
            }

            const int minimumMatchLimit = 30;

            var scoredResults = FuzzySharp.Process.ExtractSorted(input, data.Select(x => x.Key));

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
