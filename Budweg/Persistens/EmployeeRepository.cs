using Budweg.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Persistens
{
    public class EmployeeRepository
    {
        private readonly string connectionString;
        private List<Employee> employees;

        public EmployeeRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            employees = new List<Employee>();
            connectionString = config.GetConnectionString("MyDBConnection")!;
        }

        public void AddEmployee(Employee employee)
        {
            string query = @"INSERT INTO Employee DEFAULT VALUES";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<Employee> GetAllEmployees()
        {
            employees.Clear();

            string query = @"SELECT EmployeeID
                             FROM Employee";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Employee employee = new Employee
                {
                    EmployeeID = Convert.ToInt32(reader["EmployeeID"])
                };

                employees.Add(employee);
            }

            return employees;
        }

        public Employee? GetEmployeeById(int employeeID)
        {
            Employee? employee = null;

            string query = @"SELECT EmployeeID
                             FROM Employee
                             WHERE EmployeeID = @EmployeeID";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@EmployeeID", employeeID);

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                employee = new Employee
                {
                    EmployeeID = Convert.ToInt32(reader["EmployeeID"])
                };
            }

            return employee;
        }
    }
}
