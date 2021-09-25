using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloHeart
{
    public class BloodTestResponse
    {
        public string Result { get; set; }
        public DiagnoseStatus ResultEvaluation { get; set; }
    }

    public enum DiagnoseStatus
    {
        Unknown,
        Good,
        Bad
    }
}
