using System.Data;
using MeterReadingModel;
using Microsoft.EntityFrameworkCore;

namespace MeterReadingRepository.Dapper
{
    public class DapperDBContext : DbContext, IApplicationDbContext
    {
        public DapperDBContext() { }
        public DapperDBContext(DbContextOptions<DapperDBContext> options) : base(options) { }
        public IDbConnection Connection { get; }
        public DbSet<MeterReading> MeterReadings { get; set; }
      
    }
}
