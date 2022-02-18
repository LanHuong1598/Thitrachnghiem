using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Thitrachnghiem.Thongke
{
    public class BackupService
    {
        public string backup()
        {
            SqlConnection sqlconn = new SqlConnection(Settings.conStr);
            SqlCommand sqlcmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            // Backup destibation
            string backupDestination = Settings.backupStr;
            // check if backup folder exist, otherwise create it.
            if (!System.IO.Directory.Exists(backupDestination))
            {
                System.IO.Directory.CreateDirectory(Settings.backupStr);
            }
            try
            {
                string thoigian = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string query = "backup database THITRACNGHIEM to disk =" + " '" + backupDestination + "\\" + thoigian + ".bak" + "'";
                sqlconn.Open();
                sqlcmd = new SqlCommand(query, sqlconn);
                sqlcmd.ExecuteNonQuery();
                //Close connection
                sqlconn.Close();
                return backupDestination + "\\" + thoigian + ".bak";

            }
            catch (Exception ex)
            {
                return "";

            }
        }
    }
}
