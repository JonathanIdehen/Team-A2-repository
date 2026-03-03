using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Model
{
    public class Caliper
    {
        public int CaliperID { get; set; }
        public int ItemNumber { get; set; }
        public string CaliperType { get; set; }

        public Caliper(int caliperID) // Constructor
        {
            CaliperID = caliperID;
        }

        public override string ToString()
        {
            return $"{CaliperID}, {ItemNumber}, {CaliperType}";
        }
    }
}
