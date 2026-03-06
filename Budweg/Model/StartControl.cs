using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Model
{
    public class StartControl
    {
        public int StartControlID { get; set; }
        public DateTime Date { get; set; }
        public int CaliperID { get; set; } // for at koble startkontrollen til en specifik kaliber
        public int EmployeeID { get; set; } //  for at koble startkontrollen til en specifik medarbejder

        public StartControl(int startControlID) // Constructor
        {
            StartControlID = startControlID;
        }

        public override string ToString()
        {
            return $"{StartControlID}, {Date}, {CaliperID}, {EmployeeID}";
        }
    }
}
