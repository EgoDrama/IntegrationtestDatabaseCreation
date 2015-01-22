using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EndlessLobster.Domain.Repository
{
    public interface IDatabaseFactory
    {
        IDbConnection GetConnection();
    }

    public class DatabaseFactory : IDatabaseFactory
    {
        public IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["ChinookStore"].ConnectionString);        
        }
    }
}