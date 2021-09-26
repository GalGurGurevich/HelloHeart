﻿using HelloHeart.Model;
using System.Collections.Generic;

namespace HelloHeart.Helpers
{
    public interface IUserBloodTestAnalyzer
    {
        public string DiagnoseBloodTest(string input, BloodTestConfigResponse map);
        public DiagnoseStatus DiagnoseCondition(string key, BloodTestConfigResponse map, int input);
    }
}