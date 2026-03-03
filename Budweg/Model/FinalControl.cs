using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Model
{
    public class FinalControl
    {
        public int FinalControlID { get; set; }
        public DateTime Date { get; set; }
        public Boolean Result { get; set; }
        public string Comment { get; set; }
        public int Waste { get; set; }
        public int Export { get; set; }

        public FinalControl(int finalControlID) // Constructor
        {
            FinalControlID = finalControlID;
        }

        public override string ToString()
        {
            return $"{FinalControlID}, {Date}, {Result}, {Comment}, {Waste}, {Export}";
        }
    }
}
