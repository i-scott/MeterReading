using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;
using MeterReadingModel;

namespace MeterReadingRepository
{
    internal interface IApplicationDbContext
    {
        public IDbConnection Connection { get; }
        DatabaseFacade Database { get; }
        public DbSet<MeterReading> MeterReadings { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
