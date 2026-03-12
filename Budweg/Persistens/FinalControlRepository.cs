using Budweg.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Budweg.Persistens
{
        public class FinalControlRepository
        {
            private readonly string connectionString; 
            private List<FinalControl> finalControls; 

            public FinalControlRepository()
            {
                IConfigurationRoot config = new ConfigurationBuilder() 
                    .AddJsonFile("appsettings.json")
                    .Build();

                finalControls = new List<FinalControl>();
                connectionString = config.GetConnectionString("MyDBConnection")!;
            }

        public void AddFinalControl(FinalControl finalControl) 
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand("AddFinalControl", connection); 

            command.CommandType = CommandType.StoredProcedure;  // Angiver, at kommandoen er en stored procedure

            command.Parameters.AddWithValue("@Date", finalControl.Date);
            command.Parameters.AddWithValue("@Result", finalControl.Result);
            command.Parameters.AddWithValue("@Comment", (object?)finalControl.Comment ?? DBNull.Value);
            command.Parameters.AddWithValue("@Waste", finalControl.Waste);
            command.Parameters.AddWithValue("@Export", finalControl.Export);
            command.Parameters.AddWithValue("@CaliperID", finalControl.CaliperID);
            command.Parameters.AddWithValue("@EmployeeID", finalControl.EmployeeID);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<FinalControl> GetAllFinalControls() // metode til at hente alle slutkontroller fra databasen
        {
                finalControls.Clear(); 

                string query = @"SELECT FinalControlID, [Date], Result, Comment, Waste, Export, CaliperID, EmployeeID
                             FROM FinalControl"; // SQL query til at hente alle slutkontroller fra databasen

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) // Læser hver række i resultatsættet og opretter en FinalControl objekt for hver række, som derefter tilføjes til finalControls listen
            {
                    FinalControl finalControl = new FinalControl 
                    {
                        FinalControlID = Convert.ToInt32(reader["FinalControlID"]), // Konverterer FinalControlID til int
                        Date = Convert.ToDateTime(reader["Date"]), // Konverterer Date til DateTime
                        Result = Convert.ToBoolean(reader["Result"]), // Konverterer Result til bool
                        Comment = reader["Comment"]?.ToString(), // Håndterer Comment, hvis det er null, returneres null, ellers konverteres det til string
                        Waste = Convert.ToBoolean(reader["Waste"]), // Konverterer Waste til bool
                        Export = Convert.ToBoolean(reader["Export"]), // Konverterer Export til bool
                        CaliperID = Convert.ToInt32(reader["CaliperID"]), // Konverterer CaliperID til int
                        EmployeeID = Convert.ToInt32(reader["EmployeeID"]) // Konverterer EmployeeID til int
                    };

                    finalControls.Add(finalControl); // Tilføjer det oprettede FinalControl objekt til finalControls listen
            }

                return finalControls; // Returnerer listen med alle FinalControl objekter
        }

        // en caliber kan kun have én slutkontrol

            public FinalControl? GetFinalControlByCaliperID(int caliperID) 

        {
            FinalControl? finalControl = null;

            string query = @"SELECT FinalControlID, [Date], Result, Comment, Waste, Export, CaliperID, EmployeeID
                             FROM FinalControl
                             WHERE CaliperID = @CaliperID";

                using SqlConnection connection = new SqlConnection(connectionString);
                using SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@CaliperID", caliperID);
                connection.Open();
                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    finalControl = new FinalControl
                    {
                        FinalControlID = Convert.ToInt32(reader["FinalControlID"]),
                        Date = Convert.ToDateTime(reader["Date"]),
                        Result = Convert.ToBoolean(reader["Result"]),
                        Comment = reader["Comment"]?.ToString(),
                        Waste = Convert.ToBoolean(reader["Waste"]),
                        Export = Convert.ToBoolean(reader["Export"]),
                        CaliperID = Convert.ToInt32(reader["CaliperID"]),
                        EmployeeID = Convert.ToInt32(reader["EmployeeID"])
                    };

            }

            return finalControl; 
            }
        }
    }
