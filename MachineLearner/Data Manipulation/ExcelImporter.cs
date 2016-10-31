using MachineLearner.DB;
using System;
using System.Collections.Generic;
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

        public void ImportDataFromExcel(string excelFilePath)
        {
            dbAccess.FillTable(excelFilePath, "Zoo");
        }
    }
}