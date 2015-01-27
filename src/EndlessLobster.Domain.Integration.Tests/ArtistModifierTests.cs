using EndlessLobster.Domain.Repository;
using FluentAssertions;
using NUnit.Framework;

namespace EndlessLobster.Domain.Integration.Tests
{
    [TestFixture]
    public class ArtistModifierTests
    {
        [Test]
        public void Returns_Modified_ArtistName_Integration()
        {
            var artistRepository = new ArtistRepository();
            var artistModifier = new ArtistModifier(artistRepository);
            const int artistId = 1;

            var actual = artistModifier.ModifyArtistName(artistId, "foo");

            actual.Name.Should().Be("AC/DC - foo");
        }
    }
}