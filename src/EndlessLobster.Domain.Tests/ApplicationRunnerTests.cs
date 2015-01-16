using System.Data.SqlServerCe;
using System.IO;
using Castle.Windsor;
using EndlessLobster.Application;
using EndlessLobster.Domain.Repository;
using FluentAssertions;
using NUnit.Framework;

namespace EndlessLobster.Domain.Tests
{
    [TestFixture]
    public class ApplicationRunnerTests
    {
        private IWindsorContainer _container;
        private IDatabaseFactory _databaseFactory;
        private DatabaseHelper _databaseHelper;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _databaseFactory = new TestDatabaseFactory();
            _databaseHelper = new DatabaseHelper(_databaseFactory);

            using (var sqlCeEngine = new SqlCeEngine(_databaseFactory.GetConnection().ConnectionString))
            {
                var databasePath = _databaseHelper.GetDatabasePath();

                if (!File.Exists(databasePath))
                {
                    sqlCeEngine.CreateDatabase();
                }
            }

            _databaseHelper.ExecuteDatabaseScript("create_database.txt");
        }

        [SetUp]
        public void SetUp()
        {
            _databaseHelper.ExecuteDatabaseScript("populate_database.txt");

            var bootstrapper = new Bootstrapper(_databaseFactory);
            
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

            actual.Name.Should().Be("Foo Barfoo");
        }

        [TearDown]
        public void TearDown()
        {
            _databaseHelper.ExecuteDatabaseScript("delete_database.txt");
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            var databasePath = _databaseHelper.GetDatabasePath();

            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }
        }
    }
}