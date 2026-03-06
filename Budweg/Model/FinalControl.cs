using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Model
{
    public class FinalControl
    {
        public int FinalControlID { get; set; }
        public DateTime Date { get; set; }
        public bool Result { get; set; }
        public string Comment { get; set; }
        public bool Waste { get; set; }
        public bool Export { get; set; }
        public int CaliperID { get; set; } // for at koble slutkontrollen til en specifik kaliber
        public int EmployeeID { get; set; } //  for at koble slutkontrollen til en specifik medarbejder


        public FinalControl()
        {
        }

        public FinalControl(int finalControlID)
        {
            FinalControlID = finalControlID;
        }

        public override string ToString()
        {
            return $"{FinalControlID}, {Date}, {Result}, {Comment}, {Waste}, {Export}, {CaliperID}, {EmployeeID}";
        }
    }
}
