using Jamify.BE.Models;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Jamify.BE.Services
{
    public class SQLDataLayer : IDisposable, IDataLayer
    {
        public SQLDataLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            Connect();
        }

        private readonly IConfiguration _configuration;

        public SqlConnection Connection { get; set; }

        public void Connect()
        {
            var connectionstring = _configuration.GetConnectionString("JamifySQLSERVER");
            if (string.IsNullOrEmpty(connectionstring))
            {
                throw new Exception("Missing Connectionstring.");
            }
            Connection = new SqlConnection(connectionstring);
            Connection.Open();
        }

        public void SeedTestDataIfDbEmpty()
        {
            /*try
            {
                var sql = @"SELECT * FROM Test"; 
                using (var cmd = new SqlCommand(sql, Connection)) //
                {
                    if (cmd.ExecuteScalar() == null)
                    {
                        using (var cmd2 = new SqlCommand(@"INSERT INTO Test(Text) VALUES ('Das ist sheeesh')", Connection)) //
                        {
                            cmd2.ExecuteNonQuery(); //
                        }
                    }
                }
            }
            catch
            {
                throw new Exception("Error checking the test database");
            }*/

            Console.WriteLine("The API RUUUUUNNNNSSS");
        }

        public DataTable GetDataTable(string query)
        {
            var dt = new DataTable();
            using (var cmd = new SqlCommand(query, this.Connection))
            {
                 var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public void Dispose()
        {
            if(Connection!.State != System.Data.ConnectionState.Closed)
            {
                Connection.Close();
            }
        }

        
    }
}
