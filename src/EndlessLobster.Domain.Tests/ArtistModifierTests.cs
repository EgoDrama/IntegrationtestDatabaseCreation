using EndlessLobster.Domain.Repository;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EndlessLobster.Domain.Tests
{
    [TestFixture]
    public class ArtistModifierTests
    {
        [Test]
        public void Returns_Modified_ArtistName()
        {
            var artistRepository = new Mock<IRepository<Artist>>();
            var artist = new Artist { Name = "foo" };
            const int artistId = 1;
            artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(artist);

            var artistModifier = new ArtistModifier(artistRepository.Object);

            var actual = artistModifier.ModifyArtistName(artistId, "bar");

            actual.Name.Should().Be("foo - bar");
        }
    }
}