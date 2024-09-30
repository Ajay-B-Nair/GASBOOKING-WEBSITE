using GASSBOOKING_WEBSITE.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GASSBOOKING_WEBSITE.Repository
{
    public class RegistrationRepository
    {
        private readonly string _connectionString = @"server=LAPTOP-0LIDVBVS\SQLEXPRESS;Database=GasBooking;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true";

        public string InsertStaff(Staff staff, Login login)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            // Prepare the command to call the stored procedure
                            SqlCommand cmd = new SqlCommand("sp_StaffRegistration", con, transaction);
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add parameters for staff
                            cmd.Parameters.AddWithValue("@Staff_Name", staff.Staff_Name);
                            cmd.Parameters.AddWithValue("@Staff_Phone", staff.Staff_Phone);
                            cmd.Parameters.AddWithValue("@Staff_Email", staff.Staff_Email);
                            cmd.Parameters.AddWithValue("@Staff_Status", staff.Staff_Status);

                            // Add parameters for login
                            cmd.Parameters.AddWithValue("@UserName", login.UserName);
                            cmd.Parameters.AddWithValue("@Password", login.Password);
                            cmd.Parameters.AddWithValue("@Login_Type", login.Login_Type);

                            // Add output parameter for the new Staff_Reg_Id
                            SqlParameter outputIdParam = new SqlParameter("@NewStaffId", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputIdParam);

                            // Execute the stored procedure
                            cmd.ExecuteNonQuery();

                            // Retrieve the new Staff_Reg_Id
                            int newStaffId = (int)outputIdParam.Value;

                            // Commit the transaction
                            transaction.Commit();

                            return "Inserted Successfully with ID: " + newStaffId;
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction if there's an error
                            transaction.Rollback();
                            return "Error: " + ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }


        public string InsertCustomer(Customer customer, Login login)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            // Prepare the command to call the stored procedure
                            SqlCommand cmd = new SqlCommand("sp_CustomerRegistration", con, transaction);
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add parameters for customer
                            cmd.Parameters.AddWithValue("@Customer_Name", customer.Customer_Name);
                            cmd.Parameters.AddWithValue("@Consumer_Number", customer.Consumer_Number);
                            cmd.Parameters.AddWithValue("@Customer_Address", customer.Customer_Address);
                            cmd.Parameters.AddWithValue("@Customer_Phone", customer.Customer_Phone);
                            cmd.Parameters.AddWithValue("@Customer_Email", customer.Customer_Email);
                            cmd.Parameters.AddWithValue("@Customer_Status", customer.Customer_Status);

                            // Add parameters for login
                            cmd.Parameters.AddWithValue("@UserName", login.UserName);
                            cmd.Parameters.AddWithValue("@Password", login.Password);
                            cmd.Parameters.AddWithValue("@Login_Type", login.Login_Type);

                            // Add output parameter for the new Customer_Reg_Id
                            SqlParameter outputIdParam = new SqlParameter("@NewCustomerId", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputIdParam);

                            // Execute the stored procedure
                            cmd.ExecuteNonQuery();

                            // Retrieve the new Customer_Reg_Id
                            int newCustomerId = (int)outputIdParam.Value;

                            // Commit the transaction
                            transaction.Commit();

                            return "Inserted Successfully with ID: " + newCustomerId;
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction if there's an error
                            transaction.Rollback();
                            return "Error: " + ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

    }
}
