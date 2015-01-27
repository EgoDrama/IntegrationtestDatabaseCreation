using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using Castle.Windsor;
using EndlessLobster.Application;
using EndlessLobster.Domain.Repository;
using FluentAssertions;
using NUnit.Framework;

namespace EndlessLobster.Domain.Integration.Tests
{
    [TestFixture]
    public class ArtistModifierTests
    {
        private DatabaseHelper _databaseHelper;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _databaseHelper = new DatabaseHelper(new TestDatabaseFactory());

            if (!File.Exists(_databaseHelper.GetDatabasePath()))
            {
                using (var sqlEngine = new SqlCeEngine(ConfigurationManager.ConnectionStrings["ChinookStore"].ConnectionString))
                {
                    sqlEngine.CreateDatabase();
                }    
            }

            _databaseHelper.ExecuteDatabaseScript("create_table.txt");
        }

        [SetUp]
        public void SetUp()
        {
            _databaseHelper.ExecuteDatabaseScript("populate_table.txt");
        }

        [TearDown]
        public void TearDown()
        {
            _databaseHelper.ExecuteDatabaseScript("delete_table.txt");
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
        public void Returns_Modified_ArtistName_Integration()
        {
            using (var container = new WindsorContainer())
            {
                var testDatabaseFactory = new TestDatabaseFactory();
                var bootstrapper = new Bootstrapper(testDatabaseFactory);
                bootstrapper.Init(container);

                var artistModifier = container.Resolve<IArtistModifier>();
                const int artistId = 1;

                var actual = artistModifier.ModifyArtistName(artistId, "foo");

                actual.Name.Should().Be("Foo Bar - foo");    
            }
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