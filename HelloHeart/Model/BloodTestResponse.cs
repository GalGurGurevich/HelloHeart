﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloHeart
{
    public class BloodTestResponse
    {
        public string Result { get; set; }
        public TestValueOutput ResultEvaluation { get; set; }
    }

    public enum TestValueOutput
    {
        Unknown,
        Good,
        Bad
    }
}