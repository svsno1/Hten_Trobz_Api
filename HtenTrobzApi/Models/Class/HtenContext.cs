using Microsoft.EntityFrameworkCore;

namespace HtenTrobzApi.Models
{
    public partial class HtenContext
    {
        public HtenContext() : base(GetOptions("local"))
        {
        }

        public HtenContext(string dbName) : base(GetOptions(dbName))
        {
        }

        private static DbContextOptions GetOptions(string dbName)
        {
            string connectionString = Common.GetConnectionString(dbName);
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}

//Scaffold-DbContext "Server=NAT;Database=HTEN-EPR-CBP-NAMNGUYEN;user=sa;password=123456;Trust Server Certificate=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context "HtenContext" -f -Table H00MEMBER,Customer,Site,Truck,Drivers,MaterialList,Orders,OrderDetail