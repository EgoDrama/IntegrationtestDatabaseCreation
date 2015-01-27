//using System;
//using System.Data;
//using System.IO;
//using System.Reflection;
//using EndlessLobster.Domain.Repository;

//namespace EndlessLobster.Domain.Integration.Tests
//{
//    public class DatabaseHelper
//    {
//        private readonly IDatabaseFactory _databaseFactory;

//        public DatabaseHelper(IDatabaseFactory databaseFactory)
//        {
//            _databaseFactory = databaseFactory;
//        }

//        public void ExecuteDatabaseScript(string fileName)
//        {
//            using (var connection = _databaseFactory.GetConnection())
//            {
//                connection.Open();
//                ExecuteDatabaseScript(connection, fileName);
//            }
//        }

//        public string GetDatabasePath()
//        {
//            const string databaseName = "TestDatabase.sdf";
//            var executingAssembly = GetPathExecutingAssembly();
//            return Path.Combine(executingAssembly, databaseName);            
//        }

//        private static string GetPathExecutingAssembly()
//        {
//            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
//            var uri = new UriBuilder(codeBase);
//            var path = Uri.UnescapeDataString(uri.Path);
//            return Path.GetDirectoryName(path);
//        }

//        private static void ExecuteDatabaseScript(IDbConnection connection, string fileName)
//        {
//            var databasePath = GetPathExecutingAssembly();
//            var file = Path.Combine(databasePath, "Data", fileName);

//            var sql = ReadSqlFromFile(file);
//            var sqlCmds = sql.Split(new[] { "GO" }, int.MaxValue, StringSplitOptions.RemoveEmptyEntries);

//            foreach (var sqlCmd in sqlCmds)
//            {
//                var cmd = connection.CreateCommand();
//                cmd.CommandText = sqlCmd;
//                cmd.ExecuteNonQuery();
//            }
//        }

//        private static string ReadSqlFromFile(string sqlFilePath)
//        {
//            using (TextReader r = new StreamReader(sqlFilePath))
//            {
//                return r.ReadToEnd();
//            }
//        }
//    }
//}