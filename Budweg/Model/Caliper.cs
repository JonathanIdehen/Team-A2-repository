using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Model
{
    public class Caliper
    {
        public int CaliperID { get; set; }
        public int ItemNumber { get; set; }
        public string? CaliperType { get; set; }

        public Caliper() 
        { 
        }

        public Caliper(int caliperID) // Constructor
        {
            CaliperID = caliperID;
        }
       
        public string FindCaliperType() // metode til at finde kalibertype baseret på de sidste 4 cifre i ItemNumber, og returnere en string med kalibertypen eller "Ukendt type" hvis det ikke matcher nogen kendte typer
        {
            string itemNumberString = ItemNumber.ToString();

            if (itemNumberString.Length < 4) // Hvis ItemNumber har færre end 4 cifre, så vi returnerer "Ukendt type"
            {
                return "Ukendt type";
            }

            string lastFourDigits = itemNumberString.Substring(itemNumberString.Length - 4); // Henter de sidste 4 cifre i ItemNumber

            if (lastFourDigits == "1111") 
            {
                return "Håndbremsekaliber";
            }
            else if (lastFourDigits == "2222")
            {
                return "Elektronisk håndbremsekaliber";
            }
            else if (lastFourDigits == "3333")
            {
                return "Forhjulsbremsekaliber";
            }
            else
            {
                return "Ukendt type";
            }
        }
        
        public void UpdateCaliperType() // metode til at opdatere CaliperType baseret på ItemNumber ved at kalde FindCaliperType-metoden
        {
            CaliperType = FindCaliperType();
        }

        public override string ToString()
        {
            return $"{CaliperID}, {ItemNumber}, {CaliperType}";
        }
    }
}
