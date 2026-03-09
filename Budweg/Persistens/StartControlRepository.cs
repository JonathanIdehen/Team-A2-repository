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
    public class StartControlRepository
    {
        private readonly string connectionString;
        private List<StartControl> startControls;

        public StartControlRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            startControls = new List<StartControl>();
            connectionString = config.GetConnectionString("MyDBConnection")!; // ! betyder at vi er sikre på at connection string ikke er null, da vi har defineret den i appsettings.json
        }

        public void AddStartControl(StartControl startControl)
        {
            string query = @"INSERT INTO StartControl
                            ([Date], CaliperID, EmployeeID)
                            VALUES
                            (@Date, @CaliperID, @EmployeeID)";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Date", startControl.Date);
            command.Parameters.AddWithValue("@CaliperID", startControl.CaliperID);
            command.Parameters.AddWithValue("@EmployeeID", startControl.EmployeeID);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public StartControl? GetStartControlByCaliperID(int caliperID) // ? fordi det kan returnere null hvis ingen StartControl findes for det givne CaliperID
        {
            StartControl? startControl = null; // betyder at startControl kan være null, indtil vi finder en matchende StartControl i databasen

            string query = @"SELECT StartControlID, [Date], CaliperID, EmployeeID
                             FROM StartControl
                             WHERE CaliperID = @CaliperID";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CaliperID", caliperID);

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                startControl = new StartControl
                {
                    StartControlID = Convert.ToInt32(reader["StartControlID"]),
                    Date = Convert.ToDateTime(reader["Date"]),
                    CaliperID = Convert.ToInt32(reader["CaliperID"]),
                    EmployeeID = Convert.ToInt32(reader["EmployeeID"])
                };
            }

            return startControl;
        }

        public List<StartControl> GetAllStartControls()
        {
            startControls.Clear();

            string query = @"SELECT StartControlID, [Date], CaliperID, EmployeeID
                             FROM StartControl";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                StartControl startControl = new StartControl
                {
                    StartControlID = Convert.ToInt32(reader["StartControlID"]),
                    Date = Convert.ToDateTime(reader["Date"]),
                    CaliperID = Convert.ToInt32(reader["CaliperID"]),
                    EmployeeID = Convert.ToInt32(reader["EmployeeID"])
                };

                startControls.Add(startControl); // Tilføjer det oprettede StartControl objekt til startControls listen
            }

            return startControls;
        }
    }
}
