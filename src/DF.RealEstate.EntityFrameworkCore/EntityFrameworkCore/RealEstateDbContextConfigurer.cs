using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace DF.RealEstate.EntityFrameworkCore
{
    public static class RealEstateDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<RealEstateDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<RealEstateDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}