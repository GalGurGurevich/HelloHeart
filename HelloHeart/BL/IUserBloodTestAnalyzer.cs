using HelloHeart.Model;
using System.Collections.Generic;

namespace HelloHeart.Helpers
{
    public interface IUserBloodTestAnalyzer
    {
        public string DiagnoseBloodTest(string input, Dictionary<string, int> map);
        public DiagnoseStatus DiagnoseCondition(int input, int treshold);
    }
}