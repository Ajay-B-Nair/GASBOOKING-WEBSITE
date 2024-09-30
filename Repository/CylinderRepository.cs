using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using GASSBOOKING_WEBSITE.Models;

namespace GASSBOOKING_WEBSITE.Repositories
{
    public class CylinderRepository
    {
        private readonly string _connectionString = @"server=LAPTOP-0LIDVBVS\SQLEXPRESS;Database=GasBooking;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true";

        public IEnumerable<Cylinder> GetAllCylinders()
        {
            var cylinders = new List<Cylinder>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT [Cylinder_Id], [Cylinder_Type], [Cylinder_TotalStock], [Filled_Cylinder], [Empty_Cylinder], [Amount] FROM [GasBooking].[dbo].[CYLINDER]";

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cylinder = new Cylinder
                            {
                                CylinderId = reader.GetInt32(0),
                                CylinderType = reader.GetString(1),
                                CylinderTotalStock = reader.GetInt32(2),
                                FilledCylinder = reader.GetInt32(3),
                                EmptyCylinder = reader.GetInt32(4),
                                Amount = reader.GetInt32(5)
                            };

                            cylinders.Add(cylinder);
                        }
                    }
                }
            }

            return cylinders;
        }

        public void AddCylinder(Cylinder cylinder)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO [GasBooking].[dbo].[CYLINDER] ([Cylinder_Type], [Cylinder_TotalStock], [Filled_Cylinder], [Empty_Cylinder], [Amount]) VALUES (@CylinderType, @CylinderTotalStock, @FilledCylinder, @EmptyCylinder, @Amount)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CylinderType", cylinder.CylinderType);
                    command.Parameters.AddWithValue("@CylinderTotalStock", cylinder.CylinderTotalStock);
                    command.Parameters.AddWithValue("@FilledCylinder", cylinder.FilledCylinder);
                    command.Parameters.AddWithValue("@EmptyCylinder", cylinder.EmptyCylinder);
                    command.Parameters.AddWithValue("@Amount", cylinder.Amount);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCylinder(Cylinder cylinder)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE [GasBooking].[dbo].[CYLINDER] SET [Cylinder_Type] = @CylinderType, [Cylinder_TotalStock] = @CylinderTotalStock, [Filled_Cylinder] = @FilledCylinder, [Empty_Cylinder] = @EmptyCylinder, [Amount] = @Amount WHERE [Cylinder_Id] = @CylinderId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CylinderId", cylinder.CylinderId);
                    command.Parameters.AddWithValue("@CylinderType", cylinder.CylinderType);
                    command.Parameters.AddWithValue("@CylinderTotalStock", cylinder.CylinderTotalStock);
                    command.Parameters.AddWithValue("@FilledCylinder", cylinder.FilledCylinder);
                    command.Parameters.AddWithValue("@EmptyCylinder", cylinder.EmptyCylinder);
                    command.Parameters.AddWithValue("@Amount", cylinder.Amount);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCylinder(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "DELETE FROM [GasBooking].[dbo].[CYLINDER] WHERE [Cylinder_Id] = @CylinderId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CylinderId", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public Cylinder GetCylinderById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT [Cylinder_Id], [Cylinder_Type], [Cylinder_TotalStock], [Filled_Cylinder], [Empty_Cylinder], [Amount] FROM [GasBooking].[dbo].[CYLINDER] WHERE [Cylinder_Id] = @CylinderId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CylinderId", id);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Cylinder
                            {
                                CylinderId = reader.GetInt32(0),
                                CylinderType = reader.GetString(1),
                                CylinderTotalStock = reader.GetInt32(2),
                                FilledCylinder = reader.GetInt32(3),
                                EmptyCylinder = reader.GetInt32(4),
                                Amount = reader.GetInt32(5)
                            };
                        }
                    }
                }
            }
            return null; // Return null if no cylinder found
        }

        public async Task<IEnumerable<Cylinder>> GetCylinderTypesAsync()
        {
            var cylinders = new List<Cylinder>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetCylinderTypes", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var cylinder = new Cylinder
                            {
                                CylinderId = reader.GetInt32(0),
                                CylinderType = reader.GetString(1)
                            };
                            cylinders.Add(cylinder);
                        }
                    }
                }
            }
            return cylinders;
        }

        public async Task<Cylinder> GetCylinderByIdAsync(int cylinderId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Cylinders WHERE CylinderId = @CylinderId", connection);
                command.Parameters.AddWithValue("@CylinderId", cylinderId);

                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Cylinder
                        {
                            CylinderId = reader.GetInt32(reader.GetOrdinal("CylinderId")),
                            CylinderType = reader.GetString(reader.GetOrdinal("CylinderType")),
                            CylinderTotalStock = reader.GetInt32(reader.GetOrdinal("CylinderTotalStock")),
                            FilledCylinder = reader.GetInt32(reader.GetOrdinal("FilledCylinder")),
                            EmptyCylinder = reader.GetInt32(reader.GetOrdinal("EmptyCylinder")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Amount"))
                        };
                    }
                }
            }
            return null; // Return null if not found
        }
    }

}
