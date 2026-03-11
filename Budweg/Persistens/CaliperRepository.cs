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

        public List<CaliperHistory> GetLatestCaliperHistory()
        {
            List<CaliperHistory> historyList = new List<CaliperHistory>();

            string query = @"
    SELECT 
        c.CaliperID,
        c.ItemNumber,

        sc.Date AS StartControlDate,
        sc.EmployeeID AS StartControlEmployeeID,

        fc.Date AS FinalControlDate,
        fc.EmployeeID AS FinalControlEmployeeID,
        fc.Result,
        fc.Waste,
        fc.Export,
        fc.Comment

    FROM Caliper c
    LEFT JOIN StartControl sc ON c.CaliperID = sc.CaliperID
    LEFT JOIN FinalControl fc ON c.CaliperID = fc.CaliperID
    ORDER BY c.CaliperID DESC";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                CaliperHistory history = new CaliperHistory
                {
                    CaliperID = Convert.ToInt32(reader["CaliperID"]),
                    ItemNumber = Convert.ToInt32(reader["ItemNumber"]),

                    StartControlDate = reader["StartControlDate"] == DBNull.Value
         ? null
         : Convert.ToDateTime(reader["StartControlDate"]),

                    StartControlEmployeeID = reader["StartControlEmployeeID"] == DBNull.Value
         ? null
         : Convert.ToInt32(reader["StartControlEmployeeID"]),

                    FinalControlDate = reader["FinalControlDate"] == DBNull.Value
         ? null
         : Convert.ToDateTime(reader["FinalControlDate"]),

                    FinalControlEmployeeID = reader["FinalControlEmployeeID"] == DBNull.Value
         ? null
         : Convert.ToInt32(reader["FinalControlEmployeeID"]),

                    ResultText = reader["Result"] == DBNull.Value
         ? null
         : (Convert.ToBoolean(reader["Result"]) ? "Godkendt" : "Ikke godkendt"),

                    Waste = reader["Waste"] == DBNull.Value
         ? null
         : Convert.ToBoolean(reader["Waste"]),

                    Export = reader["Export"] == DBNull.Value
         ? null
         : Convert.ToBoolean(reader["Export"]),

                    Comment = reader["Comment"] == DBNull.Value
         ? null
         : reader["Comment"].ToString()
                };

                historyList.Add(history);
            }

            return historyList;
        }

        public CaliperHistory? GetCaliperHistoryById(int caliperID)
        {
            CaliperHistory? history = null;

            string query = @"
    SELECT 
        c.CaliperID,
        c.ItemNumber,

        sc.Date AS StartControlDate,
        sc.EmployeeID AS StartControlEmployeeID,

        fc.Date AS FinalControlDate,
        fc.EmployeeID AS FinalControlEmployeeID,
        fc.Result,
        fc.Waste,
        fc.Export,
        fc.Comment

    FROM Caliper c
    LEFT JOIN StartControl sc ON c.CaliperID = sc.CaliperID
    LEFT JOIN FinalControl fc ON c.CaliperID = fc.CaliperID
    WHERE c.CaliperID = @CaliperID";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CaliperID", caliperID);

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                history = new CaliperHistory
                {
                    CaliperID = Convert.ToInt32(reader["CaliperID"]),
                    ItemNumber = Convert.ToInt32(reader["ItemNumber"]),

                    StartControlDate = reader["StartControlDate"] == DBNull.Value
         ? null
         : Convert.ToDateTime(reader["StartControlDate"]),

                    StartControlEmployeeID = reader["StartControlEmployeeID"] == DBNull.Value
         ? null
         : Convert.ToInt32(reader["StartControlEmployeeID"]),

                    FinalControlDate = reader["FinalControlDate"] == DBNull.Value
         ? null
         : Convert.ToDateTime(reader["FinalControlDate"]),

                    FinalControlEmployeeID = reader["FinalControlEmployeeID"] == DBNull.Value
         ? null
         : Convert.ToInt32(reader["FinalControlEmployeeID"]),

                    ResultText = reader["Result"] == DBNull.Value
         ? null
         : (Convert.ToBoolean(reader["Result"]) ? "Godkendt" : "Ikke godkendt"),

                    Waste = reader["Waste"] == DBNull.Value
         ? null
         : Convert.ToBoolean(reader["Waste"]),

                    Export = reader["Export"] == DBNull.Value
         ? null
         : Convert.ToBoolean(reader["Export"]),

                    Comment = reader["Comment"] == DBNull.Value
         ? null
         : reader["Comment"].ToString()
                };
            }

            return history;
        }
    }
}
