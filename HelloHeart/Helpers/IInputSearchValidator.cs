﻿using HelloHeart.Model;
using System.Collections.Generic;

namespace HelloHeart.Helpers
{
    public interface IInputSearchValidator
    {
        public string DiagnoseBloodTest(string input, BloodTestConfigResponse map);
        public DiagnoseStatus DiagnoseCondition(string key, BloodTestConfigResponse map, int input);
        public string SearchStringMatchInCollection(IEnumerable<string> keys, IEnumerable<string> collection);
        public string CleanStringFromSymbols(string input);
    }
}