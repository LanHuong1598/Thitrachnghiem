using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem
{
    public class Settings
    {
        public static string Secret = "marcy9d8534b48w951b9287d492b256x";
        public static string conStr;
        public static string backupStr;

        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(conStr);
                return connection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
