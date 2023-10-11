using System.Linq;
using Dapper;

namespace MeterReadingRepository.Dapper
{
    public class DapperDatabase
    {
        private readonly DapperDBContext _context;

        public DapperDatabase(DapperDBContext context)
        {
            _context = context;
        }

        public void CreateDatabase(string dbName)
        {
            var query = "SELECT * FROM sys.databases WHERE name = @name";
            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);
            using (var connection = _context.CreateMasterConnection())
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                    connection.Execute($"CREATE DATABASE {dbName}");
            }
        }
    }
}
