using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace CheckExpiredDate
{
    class Program
    {
        static void Main(string[] args)
        {
            //RentalHouseFinding
            string connectionString = "data source=localhost;initial catalog=RentalHouseFinding;persist security info=True; Integrated Security=SSPI;";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand("ChangeStatusPostWhenExpride", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@statusId", SqlDbType.Int).Value = 4;// depent on Expride Id in StatusPost.
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
                Console.WriteLine("------------DONE------------");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                
            }
        }
    }
}
