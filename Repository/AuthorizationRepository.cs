//using Microsoft.Data.SqlClient;
//using System.Data;

//namespace GASSBOOKING_WEBSITE.Repositories
//{
//    public class AuthorizationRepository
//    {
//        private readonly string _connectionString = @"server=LAPTOP-0LIDVBVS\SQLEXPRESS;Database=GasBooking;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true";

//        public bool ValidateUser(string userName, string password, out string userType, out int registerId)
//        {
//            userType = null;
//            registerId = 0; // Initialize registerId

//            using (var connection = new SqlConnection(_connectionString))
//            {
//                using (var command = new SqlCommand("ValidateUser", connection))
//                {
//                    command.CommandType = CommandType.StoredProcedure;

//                    // Adding parameters
//                    command.Parameters.AddWithValue("@UserName", userName);
//                    command.Parameters.AddWithValue("@Password", password); // Ensure password hashing

//                    var userTypeParam = new SqlParameter("@UserType", SqlDbType.NVarChar, 50)
//                    {
//                        Direction = ParameterDirection.Output
//                    };
//                    command.Parameters.Add(userTypeParam);

//                    var registerIdParam = new SqlParameter("@RegisterId", SqlDbType.Int)
//                    {
//                        Direction = ParameterDirection.Output
//                    };
//                    command.Parameters.Add(registerIdParam);

//                    connection.Open();
//                    command.ExecuteNonQuery();

//                    // Get the output parameter values
//                    userType = (string)userTypeParam.Value;
//                    registerId = (int)(registerIdParam.Value ?? 0); // Handle null case

//                    return !string.IsNullOrEmpty(userType);
//                }
//            }
//        }
//    }
//}

using Microsoft.Data.SqlClient;
using System.Data;

namespace GASSBOOKING_WEBSITE.Repositories
{
    public class AuthorizationRepository
    {
        private readonly string _connectionString = @"server=LAPTOP-0LIDVBVS\SQLEXPRESS;Database=GasBooking;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true";

        public bool ValidateUser(string userName, string password, out string userType, out int customerRegId)
        {
            userType = null;
            customerRegId = 0; // Initialize customerRegId

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("ValidateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Adding parameters
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@Password", password); // Ensure password hashing in the stored procedure

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

                    // Get the output parameter values
                    userType = (string)userTypeParam.Value;
                    customerRegId = (int)(customerRegIdParam.Value ?? 0); // Handle null case

                    return !string.IsNullOrEmpty(userType); // Return true if the user type is valid
                }
            }
        }
    }
}

