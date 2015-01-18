using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace EndlessLobster.Domain.Repository
{
    public class ArtistRepository : IRepository<Artist>
    {
        public Artist Get(int id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ChinookStore"].ConnectionString))
            {
                const string query = "SELECT ArtistId, Name FROM Artist WHERE ArtistId = @id";
                return connection.Query<Artist>(query, new { id }).SingleOrDefault();
            }
        }
    }
}