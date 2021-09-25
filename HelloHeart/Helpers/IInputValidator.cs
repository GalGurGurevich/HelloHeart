using HelloHeart.Model;
using System.Collections.Generic;

namespace HelloHeart.Helpers
{
    public interface IInputValidator
    {
        public string ExtractKey(string input, BloodTestConfigResponse map);
        public TestValueOutput ExtractValue(string key, BloodTestConfigResponse map, int input);
        public string SearchStringMatchInCollection(IEnumerable<string> keys, IEnumerable<string> collection);
        public string CleanStringFromSymbols(string input);
    }
}