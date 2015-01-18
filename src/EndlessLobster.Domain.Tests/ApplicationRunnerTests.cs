using EndlessLobster.Domain.Repository;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EndlessLobster.Domain.Tests
{
    [TestFixture]
    public class ApplicationRunnerTests
    {
        [Test]
        public void Returns_Modified_Artist()
        {
            var artistRepository = new Mock<IRepository<Artist>>();
            var artist = new Artist { Name = "Foo" };
            artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(artist);

            var artistModifier = new ArtistModifier(artistRepository.Object);

            var actual = artistModifier.ModifyArtist("bar");

            actual.Name.Should().Be("Foobar");
        }
    }
}