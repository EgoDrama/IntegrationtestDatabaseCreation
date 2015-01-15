using System.Configuration;
using System.Data.SqlClient;

namespace EndlessLobster.Domain.Repository
{
    public interface IDatabaseFactory
    {
        SqlConnection GetConnection();
    }

    public class DatabaseFactory : IDatabaseFactory
    {
        public SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["ChinookStore"].ConnectionString);
        }
    }
}