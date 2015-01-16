using EndlessLobster.Domain.Repository;

namespace EndlessLobster.Domain
{
    public class ArtistModifier
    {
        private readonly IRepository<Artist> _artistRepository;

        public ArtistModifier(IRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public Artist ModifyArtist(string nameToAdd)
        {
            int artistId = 1;
            var artist = _artistRepository.Get(artistId);
            artist.Name += nameToAdd;

            return artist;
        }
    }
}