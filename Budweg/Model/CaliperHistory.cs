using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Model
{
    public class CaliperHistory
    {
        public int CaliperID { get; set; }
        public int ItemNumber { get; set; }

        public DateTime? StartControlDate { get; set; }
        public int? StartControlEmployeeID { get; set; }

        public DateTime? FinalControlDate { get; set; }
        public int? FinalControlEmployeeID { get; set; }

        public string? ResultText { get; set; }
        public string? Comment { get; set; }

        public bool? Waste { get; set; }
        public bool? Export { get; set; }
    }
}
