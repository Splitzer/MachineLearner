using MachineLearner.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MachineLearner
{
    public class ExcelImporter
    {
        DB_Access dbAccess;

        public ExcelImporter()
        {
            dbAccess = new DB_Access();
        }

        public DataTable ExcelToDatatable()
        {
            string sSheetName = null;
            string sConnection = null;
            DataTable dtTablesList = default(DataTable);
            OleDbCommand oleExcelCommand = default(OleDbCommand);
            OleDbDataReader oleExcelReader = default(OleDbDataReader);
            OleDbConnection oleExcelConnection = default(OleDbConnection);

            sConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Test.xls;Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";

            oleExcelConnection = new OleDbConnection(sConnection);
            oleExcelConnection.Open();

            dtTablesList = oleExcelConnection.GetSchema("Tables");

            if (dtTablesList.Rows.Count > 0)
            {
                sSheetName = dtTablesList.Rows[0]["TABLE_NAME"].ToString();
            }

            dtTablesList.Clear();
            dtTablesList.Dispose();


            if (!string.IsNullOrEmpty(sSheetName))
            {
                oleExcelCommand = oleExcelConnection.CreateCommand();
                oleExcelCommand.CommandText = "Select * From [" + sSheetName + "]";
                oleExcelCommand.CommandType = CommandType.Text;
                oleExcelReader = oleExcelCommand.ExecuteReader();

                while (oleExcelReader.Read())
                {
                }
                oleExcelReader.Close();
            }
            oleExcelConnection.Close();
        }

        public void ImportDataFromExcel(string excelFilePath)
        {
            dbAccess.CreateDatasetTable(new DataTable());
            dbAccess.FillTable(excelFilePath, "Zoo");
        }
    }
}