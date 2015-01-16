using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using Castle.Windsor;
using EndlessLobster.Application;
using EndlessLobster.Domain.Repository;
using NUnit.Framework;
using FluentAssertions;

namespace EndlessLobster.Domain.Tests
{
    [TestFixture]
    public class ApplicationRunnerTests
    {
        private IWindsorContainer _container;

        [SetUp]
        public void SetUp()
        {
            var databaseFactory = new DatabaseFactory();
            var bootstrapper = new Bootstrapper(databaseFactory);
            
            using (var container = new WindsorContainer())
            {
                bootstrapper.Init(container);
                _container = bootstrapper.Container;    
            }
        }

        [Test]
        public void Returns_Modified_Artist()
        {
            var artistRepository = _container.Resolve<IRepository<Artist>>();
            var artistModifier = new ArtistModifier(artistRepository);

            var actual = artistModifier.ModifyArtist("foo");

            actual.Name.Should().Be("AC/DCfoo");
        }
    }

    public class TestDatabaseFactory : IDatabaseFactory
    {
        public IDbConnection GetConnection()
        {
            return new SqlCeConnection(ConfigurationManager.ConnectionStrings["ChinookStore"].ConnectionString);
        }
    }
}