using System.Data;
using System.Data.SqlClient;

namespace MachineLearner.DB
{
    class DB_Access
    {
        SqlConnection connection;

        public DB_Access()
        {
            connection = DB_Connection.GetConnection();
        }

        public DataTable DownloadDb()
        {
            DataTable table = new DataTable("Shedule");

            if (connection.State.ToString() == "Closed")
            {
                connection.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Shedule";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);

            connection.Close();
            return table;
        }

        public DataRow GetConstants()
        {
            DataTable table = new DataTable("Constants");

            if (connection.State.ToString() == "Closed")
            {
                connection.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Constants";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);

            DataRow row = table.Rows[0];

            connection.Close();
            return row;
        }
    }
}