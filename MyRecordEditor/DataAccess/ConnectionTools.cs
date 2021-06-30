using System.Configuration;

namespace MyRecordEditor.DataAccess
{
    public static class ConnectionTools
    {
        public static string GetConnectionString(string name = "MyJukebox")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
