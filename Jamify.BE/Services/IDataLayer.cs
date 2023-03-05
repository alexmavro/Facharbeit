using Microsoft.Data.SqlClient;
using System.Data;

namespace Jamify.BE.Services
{
    public interface IDataLayer
    {
        public SqlConnection? Connection { get; set; }
        public void Connect();
        public DataTable GetDataTable(string query);
    }
}
