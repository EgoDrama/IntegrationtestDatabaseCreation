using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;

namespace EndlessLobster.Domain.Integration.Tests
{
    public class TestDatabaseFactory
    {
        public IDbConnection GetConnection()
        {
            return new SqlCeConnection(ConfigurationManager.ConnectionStrings["ChinookStore"].ConnectionString);
        }
    }
}