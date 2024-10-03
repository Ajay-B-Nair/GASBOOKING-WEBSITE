using Microsoft.Data.SqlClient;
using System.Data;

namespace GASSBOOKING_WEBSITE.Repositories
{
    public class AuthorizationRepository
    {
        private readonly string _connectionString = @"server=LAPTOP-0LIDVBVS\SQLEXPRESS;Database=GasBooking;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true";

        public bool ValidateUser(string userName, string password, out string? userType, out int customerRegId)
        {
            userType = null;
            customerRegId = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("ValidateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@Password", password);

                    var userTypeParam = new SqlParameter("@UserType", SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(userTypeParam);

                    var customerRegIdParam = new SqlParameter("@CustomerRegId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(customerRegIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    userType = userTypeParam.Value != DBNull.Value ? userTypeParam.Value as string : null;

                    if (customerRegIdParam.Value != DBNull.Value)
                    {
                        customerRegId = (int)customerRegIdParam.Value;
                    }
                    else
                    {
                        customerRegId = 0; // Default value for admin
                    }

                    return !string.IsNullOrEmpty(userType);
                }
            }
        }
    }
}
