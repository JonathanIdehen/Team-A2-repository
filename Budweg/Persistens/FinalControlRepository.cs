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
                connectionString = config.GetConnectionString("MyDBConnection");
            }

            public void AddFinalControl(FinalControl finalControl) // metode til at tilføje en final control til databasen
        {
                string query = @"INSERT INTO FinalControl 
                            ([Date], Result, Comment, Waste, Export, CaliperID, EmployeeID) 
                            VALUES
                            (@Date, @Result, @Comment, @Waste, @Export, @CaliperID, @EmployeeID)"; // SQL query til at indsætte en ny final control i databasen

            using SqlConnection connection = new SqlConnection(connectionString); // Opretter en forbindelse til databasen
            using SqlCommand command = new SqlCommand(query, connection); // Opretter en SQL kommando med den tidligere definerede query og forbindelsen

            command.Parameters.AddWithValue("@Date", finalControl.Date); // Tilføjer Date parameter
            command.Parameters.AddWithValue("@Result", finalControl.Result); // Tilføjer Result parameter
            command.Parameters.AddWithValue("@Comment", (object?)finalControl.Comment ?? DBNull.Value); // Tilføjer Comment parameter, håndterer null værdier ved at bruge DBNull.Value
            command.Parameters.AddWithValue("@Waste", finalControl.Waste); // Tilføjer Waste parameter
            command.Parameters.AddWithValue("@Export", finalControl.Export); // Tilføjer Export parameter
            command.Parameters.AddWithValue("@CaliperID", finalControl.CaliperID); // Tilføjer CaliperID parameter
            command.Parameters.AddWithValue("@EmployeeID", finalControl.EmployeeID); // Tilføjer EmployeeID parameter

            connection.Open(); // Åbner forbindelsen til databasen
            command.ExecuteNonQuery(); // Udfører SQL kommandoen, i dette tilfælde en INSERT, som ikke returnerer nogen data, derfor bruges ExecuteNonQuery
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

            public List<FinalControl> GetFinalControlsByCaliperID(int caliperID)
            {
                List<FinalControl> resultList = new List<FinalControl>();

                string query = @"SELECT FinalControlID, [Date], Result, Comment, Waste, Export, CaliperID, EmployeeID
                             FROM FinalControl
                             WHERE CaliperID = @CaliperID";

                using SqlConnection connection = new SqlConnection(connectionString);
                using SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@CaliperID", caliperID);

                connection.Open();
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    FinalControl finalControl = new FinalControl
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

                    resultList.Add(finalControl);
                }

                return resultList;
            }
        }
    }
