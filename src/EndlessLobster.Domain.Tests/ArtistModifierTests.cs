using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using EndlessLobster.Domain.Repository;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EndlessLobster.Domain.Tests
{
    [TestFixture]
    public class ArtistModifierTests
    {
        private IDatabaseFactory _databaseFactory;
        private DatabaseHelper _databaseHelper;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _databaseFactory = new FakeDatabaseFactory();
            _databaseHelper = new DatabaseHelper(_databaseFactory);
            
            using (var connection = new SqlCeEngine(_databaseFactory.GetConnection().ConnectionString))
            {
                if (!File.Exists(_databaseHelper.GetDatabasePath()))
                {
                    connection.CreateDatabase();    
                }
            }
        }

        [SetUp]
        public void SetUp()
        {
            _databaseHelper.ExecuteDatabaseScript("create_database.txt");
            _databaseHelper.ExecuteDatabaseScript("populate_database.txt");
        }

        [TearDown]
        public void TearDown()
        {
            _databaseHelper.ExecuteDatabaseScript("delete_database.txt");
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            if (File.Exists(_databaseHelper.GetDatabasePath()))
            {
                File.Delete(_databaseHelper.GetDatabasePath());
            }
        }

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

        [Test]
        public void Returns_Modified_ArtistName_Integration()
        {
            
            var artistRepository = new ArtistRepository(_databaseFactory);
            var artistModifier = new ArtistModifier(artistRepository);
            const int artistId = 1;

            var actual = artistModifier.ModifyArtistName(artistId, "foo");

            actual.Name.Should().Be("Foo Bar - foo");
        }
    }

    public class FakeDatabaseFactory : IDatabaseFactory
    {
        public IDbConnection GetConnection()
        {
            return new SqlCeConnection(ConfigurationManager.ConnectionStrings["ChinookStore"].ConnectionString);
        }
    }
}