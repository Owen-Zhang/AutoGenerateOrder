using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagerCenter.Model
{
    public class NormalResponse
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }
    }
}
