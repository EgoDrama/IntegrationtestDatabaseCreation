using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using EndlessLobster.Domain.Repository;

namespace EndlessLobster.Domain.Tests
{
    public class TestDatabaseFactory
    {
        public IDbConnection GetConnection()
        {
            return new SqlCeConnection(ConfigurationManager.ConnectionStrings["ChinookStore"].ConnectionString);
        }
    }
}