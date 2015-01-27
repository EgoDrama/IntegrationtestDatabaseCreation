using EndlessLobster.Domain.Repository;

namespace EndlessLobster.Domain
{
    public interface IArtistModifier
    {
        Artist ModifyArtistName(int artistId, string nameToAdd);
    }

    public class ArtistModifier : IArtistModifier
    {
        private readonly IRepository<Artist> _artistRepository;

        public ArtistModifier(IRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public Artist ModifyArtistName(int artistId, string nameToAdd)
        {
            var artist = _artistRepository.Get(artistId);
            artist.Name = string.Concat(artist.Name, " - ", nameToAdd);

            return artist;
        }
    }
}