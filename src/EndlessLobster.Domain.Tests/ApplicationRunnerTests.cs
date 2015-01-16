using System;
using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Reflection;
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
        private IDatabaseFactory _databaseFactory;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _databaseFactory = new TestDatabaseFactory();

            using (var sqlCeEngine = new SqlCeEngine(_databaseFactory.GetConnection().ConnectionString))
            {
                var databasePath = GetDatabasePath();

                if (!File.Exists(databasePath))
                {
                    sqlCeEngine.CreateDatabase();
                }
            }

            ExecuteDatabaseScript("create_database.txt");
        }

        [SetUp]
        public void SetUp()
        {
            ExecuteDatabaseScript("populate_database.txt");

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
            ExecuteDatabaseScript("delete_database.txt");
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            var databasePath = GetDatabasePath();

            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }
        }

        private string GetDatabasePath()
        {
            const string databaseName = "TestDatabase.sdf";
            var executingAssembly = GetPathExecutingAssembly();
            var databasePath = Path.Combine(executingAssembly, "Data");
            return string.Format(databaseName, databasePath);
        }

        private static string GetPathExecutingAssembly()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        private void ExecuteDatabaseScript(string fileName)
        {
            using (var connection = _databaseFactory.GetConnection())
            {
                connection.Open();
                ExecuteDatabaseScript(connection, fileName);
            }
        }

        private static void ExecuteDatabaseScript(IDbConnection connection, string fileName)
        {
            var databasePath = GetPathExecutingAssembly();
            var file = Path.Combine(databasePath, "Data", fileName);

            var sql = ReadSqlFromFile(file);
            var sqlCmds = sql.Split(new[] { "GO" }, int.MaxValue, StringSplitOptions.RemoveEmptyEntries);

            foreach (var sqlCmd in sqlCmds)
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = sqlCmd;
                cmd.ExecuteNonQuery();
            }
        }

        private static string ReadSqlFromFile(string sqlFilePath)
        {
            using (TextReader r = new StreamReader(sqlFilePath))
            {
                return r.ReadToEnd();
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