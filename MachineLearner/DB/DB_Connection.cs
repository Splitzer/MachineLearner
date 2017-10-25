using System.Data.SqlClient;
using System.Configuration;

namespace MachineLearner.DB
{
    class DB_Connection
    {
        public static SqlConnection NewCon;
        //public static string ConStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static string ConStr = ConfigurationManager.ConnectionStrings["WorkConnection"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            NewCon = new SqlConnection(ConStr);
            return NewCon;
        }
    }
}
