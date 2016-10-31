using System;
using System.Data;
using System.Data.OleDb;
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

        public DataTable GetDataset(string tableName)
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

        public void CreateDatasetTable(DataTable table)
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

        internal void FillTable(string excelFilePath, string tableName)
        {
            if (connection.State.ToString() == "Closed") connection.Open();

            ////execute a query to erase any previous data from our destination table 
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = connection;
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "DELETE FROM " + tableName;

            //cmd.ExecuteNonQuery();

            //connection.Close();

            //series of commands to bulk copy data from the excel file into our sql table 
            string myexceldataquery = "select student,rollno,course from [Sheet1$]";
            OleDbConnection oledbconn = new OleDbConnection(@"provider=microsoft.jet.oledb.4.0;data source=" + excelFilePath + ";extended properties=" + "\"excel 8.0;hdr=yes;\"");
            OleDbCommand oledbcmd = new OleDbCommand(myexceldataquery, oledbconn);
            oledbconn.Open();
            OleDbDataReader dr = oledbcmd.ExecuteReader();
            SqlBulkCopy bulkcopy = new SqlBulkCopy(connection);
            bulkcopy.DestinationTableName = tableName;

            while (dr.Read())
            {
                bulkcopy.WriteToServer(dr);
            }

            dr.Close();
            oledbconn.Close();
        }
    }
}