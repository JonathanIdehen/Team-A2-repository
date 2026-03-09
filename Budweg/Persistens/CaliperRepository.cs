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
    public class CaliperRepository
    {
        private readonly string connectionString; // connection string til databasen
        private List<Caliper> calipers; // liste til at holde caliper objekter

        public CaliperRepository() 
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            calipers = new List<Caliper>();
            connectionString = config.GetConnectionString("MyDBConnection")!;
        }

        public void AddCaliper(Caliper caliper)
        {
            caliper.UpdateCaliperType();

            string query = @"INSERT INTO Caliper (CaliperID, ItemNumber, CaliperType)
                     VALUES (@CaliperID, @ItemNumber, @CaliperType)";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CaliperID", caliper.CaliperID);
            command.Parameters.AddWithValue("@ItemNumber", caliper.ItemNumber);
            command.Parameters.AddWithValue("@CaliperType", caliper.CaliperType);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<Caliper> GetAllCalipers()
        {
            calipers.Clear();

            string query = @"SELECT CaliperID, ItemNumber, CaliperType
                             FROM Caliper";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Caliper caliper = new Caliper
                {
                    CaliperID = Convert.ToInt32(reader["CaliperID"]),
                    ItemNumber = Convert.ToInt32(reader["ItemNumber"]),
                    CaliperType = reader["CaliperType"].ToString()
                };

                calipers.Add(caliper);
            }

            return calipers;
        }

        public Caliper? GetCaliperById(int caliperID) // hvis der ikke findes en caliper med det ID, returnerer vi null.
        {
            Caliper? caliper = null;

            string query = @"SELECT CaliperID, ItemNumber, CaliperType
                             FROM Caliper
                             WHERE CaliperID = @CaliperID";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CaliperID", caliperID);

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                caliper = new Caliper
                {
                    CaliperID = Convert.ToInt32(reader["CaliperID"]),
                    ItemNumber = Convert.ToInt32(reader["ItemNumber"]),
                    CaliperType = reader["CaliperType"].ToString()! // ! fordi vi forventer at CaliperType ikke er null i databasen
                };
            }

            return caliper;
        }

        public List<Caliper> GetLatestCalipers()
        {
            List<Caliper> latestCalipers = new List<Caliper>();

            string query = @"SELECT CaliperID, ItemNumber, CaliperType
                     FROM Caliper
                     ORDER BY CaliperID DESC";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Caliper caliper = new Caliper
                {
                    CaliperID = Convert.ToInt32(reader["CaliperID"]),
                    ItemNumber = Convert.ToInt32(reader["ItemNumber"]),
                    CaliperType = reader["CaliperType"].ToString()!
                };

                latestCalipers.Add(caliper);
            }

            return latestCalipers;
        }
    }
}
