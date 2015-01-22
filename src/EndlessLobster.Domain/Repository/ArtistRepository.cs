using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace EndlessLobster.Domain.Repository
{
    public class ArtistRepository : IRepository<Artist>
    {
        private readonly IDatabaseFactory _databaseFactory;

        public ArtistRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public Artist Get(int id)
        {
            using (var connection = _databaseFactory.GetConnection())
            {
                const string query = "SELECT ArtistId, Name FROM Artist WHERE ArtistId = @id";
                return connection.Query<Artist>(query, new { id }).SingleOrDefault();
            }
        }
    }
}