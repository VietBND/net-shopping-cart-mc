using ApplicationUser.Domain.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUser.Infrastructures.Seeds
{
    public class SeedHelper
    {
        public static async Task GeneratePermissionsDataWithCsvFile(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var bulkCopy = new SqlBulkCopy(connection))
                    {
                        var datatable = new DataTable("Permissions");
                        DataColumn c = new DataColumn();        // always
                        datatable.Columns.Add(new DataColumn("Id", Type.GetType("System.Guid")));
                        datatable.Columns.Add(new DataColumn("Key", Type.GetType("System.String")));
                        datatable.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));
                        datatable.Columns.Add(new DataColumn("Description", Type.GetType("System.String")));
                        TextReader reader = new StreamReader(Path.Combine("..\\shopping-cart-infrastructures\\Seeds\\permissions.csv"));
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            NewLine = Environment.NewLine,
                        };
                        var csvReader = new CsvReader(reader, config);
                        var records = csvReader.GetRecords<Permission>();
                        DataRow dr = datatable.NewRow();
                        foreach (var record in records)
                        {
                            var id = Guid.NewGuid();
                            var key = record.Key;
                            var name = record.Name;
                            var description = record.Description;
                            dr["Id"] = id;
                            dr["Key"] = key;
                            dr["Name"] = name;
                            dr["Description"] = description;
                            Console.WriteLine($"Id: {id};Key: {key};Name: {name};Description: {description}");
                            datatable.Rows.Add(dr);
                            dr = datatable.NewRow();
                        }
                        bulkCopy.DestinationTableName = "Permissions";
                        await bulkCopy.WriteToServerAsync(datatable);
                        Console.WriteLine("Permission Bulk Copy Successfuly");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Connection dont't open");
            }
        }
        public static async Task GenerateRolesDataWithCsvFile(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var bulkCopy = new SqlBulkCopy(connection))
                    {
                        var datatable = new DataTable("Permissions");
                        DataColumn c = new DataColumn();        // always
                        datatable.Columns.Add(new DataColumn("Id", Type.GetType("System.Guid")));
                        datatable.Columns.Add(new DataColumn("Key", Type.GetType("System.String")));
                        datatable.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));
                        datatable.Columns.Add(new DataColumn("Description", Type.GetType("System.String")));
                        TextReader reader = new StreamReader(Path.Combine("..\\shopping-cart-infrastructures\\Seeds\\permissions.csv"));
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            NewLine = Environment.NewLine,
                        };
                        var csvReader = new CsvReader(reader, config);
                        var records = csvReader.GetRecords<Permission>();
                        DataRow dr = datatable.NewRow();
                        foreach (var record in records)
                        {
                            var id = Guid.NewGuid();
                            var key = record.Key;
                            var name = record.Name;
                            var description = record.Description;
                            dr["Id"] = id;
                            dr["Key"] = key;
                            dr["Name"] = name;
                            dr["Description"] = description;
                            Console.WriteLine($"Id: {id};Key: {key};Name: {name};Description: {description}");
                            datatable.Rows.Add(dr);
                            dr = datatable.NewRow();
                        }
                        bulkCopy.DestinationTableName = "Permissions";
                        await bulkCopy.WriteToServerAsync(datatable);
                        Console.WriteLine("Permission Bulk Copy Successfuly");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Connection dont't open");
            }
        }
        public static async Task GenerateUsersDataWithCsvFile(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var bulkCopy = new SqlBulkCopy(connection))
                    {
                        var datatable = new DataTable("Permissions");
                        DataColumn c = new DataColumn();        // always
                        datatable.Columns.Add(new DataColumn("Id", Type.GetType("System.Guid")));
                        datatable.Columns.Add(new DataColumn("Key", Type.GetType("System.String")));
                        datatable.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));
                        datatable.Columns.Add(new DataColumn("Description", Type.GetType("System.String")));
                        TextReader reader = new StreamReader(Path.Combine("..\\shopping-cart-infrastructures\\Seeds\\permissions.csv"));
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            NewLine = Environment.NewLine,
                        };
                        var csvReader = new CsvReader(reader, config);
                        var records = csvReader.GetRecords<Permission>();
                        DataRow dr = datatable.NewRow();
                        foreach (var record in records)
                        {
                            var id = Guid.NewGuid();
                            var key = record.Key;
                            var name = record.Name;
                            var description = record.Description;
                            dr["Id"] = id;
                            dr["Key"] = key;
                            dr["Name"] = name;
                            dr["Description"] = description;
                            Console.WriteLine($"Id: {id};Key: {key};Name: {name};Description: {description}");
                            datatable.Rows.Add(dr);
                            dr = datatable.NewRow();
                        }
                        bulkCopy.DestinationTableName = "Permissions";
                        await bulkCopy.WriteToServerAsync(datatable);
                        Console.WriteLine("Permission Bulk Copy Successfuly");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Connection dont't open");
            }
        }
    }
}
