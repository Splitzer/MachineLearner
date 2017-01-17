using MachineLearner.DB;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;

namespace MachineLearner
{
    public class ExcelImporter
    {
        DB_Access dbAccess;

        public ExcelImporter()
        {
            dbAccess = new DB_Access();
        }

        public void ImportDataFromExcel(string excelFilePath)
        {
            DataTable table = ConvertExcelToDataTable(excelFilePath);
            string fileName = (Path.GetFileNameWithoutExtension(excelFilePath)).Replace(" ", "");
            table.TableName = fileName;

            if (!dbAccess.TableExists(fileName))
            {
                dbAccess.CreateDatasetTable(table);
                dbAccess.FillTable(table);
            }
        }

        private DataTable ConvertExcelToDataTable(string filePath, bool hasHeaders = false)
        {
            DataTable dtexcel = new DataTable();
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;

            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
            {
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            }
            else
            {
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
            }

            OleDbConnection connection = new OleDbConnection(strConn);
            connection.Open();
            DataTable schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            //Looping Total Sheet of Xl File
            /*foreach (DataRow schemaRow in schemaTable.Rows)
            {
            }*/
            //Looping a first Sheet of Xl File
            DataRow schemaRow = schemaTable.Rows[0];
            string sheet = schemaRow["TABLE_NAME"].ToString();
            if (!sheet.EndsWith("_"))
            {
                string excelQuery = "SELECT  * FROM [Sheet1$]";
                OleDbDataAdapter excelDa = new OleDbDataAdapter(excelQuery, connection);
                dtexcel.Locale = CultureInfo.CurrentCulture;
                excelDa.Fill(dtexcel);
            }

            connection.Close();
            return dtexcel;
        }
    }
}