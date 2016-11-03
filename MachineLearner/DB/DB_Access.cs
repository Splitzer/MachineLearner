using System;
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

        internal DataTable GetDataset(string tableName)
        {
            DataTable table = new DataTable(tableName);

            if (connection.State.ToString() == "Closed") connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM" + tableName;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);

            connection.Close();
            return table;
        }

        internal void CreateDatasetTable(DataTable table, bool hasColumnNames = false)
        {
            if (connection.State.ToString() == "Closed") connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "CREATE TABLE " + table.TableName + Environment.NewLine + "(" + Environment.NewLine;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                cmd.CommandText += "Column" + i + " varchar(255), " + Environment.NewLine; 
            }
            cmd.CommandText += ")";

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        internal void FillTable(DataTable table)
        {
            if (connection.State.ToString() == "Closed") connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "INSERT " + table.TableName + " VALUES ";

            foreach (DataRow row in table.Rows)
            {
                cmd.CommandText += "(";
                for (int i = 0; i < table.Columns.Count - 1; i++)
                {
                    cmd.CommandText += "'" + row[i] + "', ";
                }
                cmd.CommandText += "'" + row[table.Columns.Count] + "'), " + Environment.NewLine;
            }

            cmd.CommandText = cmd.CommandText.Remove(cmd.CommandText.Length - 1);
            //INSERT sometable (a, b, c)
            //VALUES  (13, 'New York', 334),
            //        (14, 'London', 823),
            //        (15, 'Paris', 1124),
            //        (16, 'Munich', 2080))

            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}