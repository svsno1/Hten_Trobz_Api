using Microsoft.EntityFrameworkCore;

namespace HtenTrobzApi.TruckModels
{
    public partial class TruckContext
    {
        public TruckContext() : base(GetOptions("truck"))
        {
        }

        public TruckContext(string dbName) : base(GetOptions(dbName))
        {
        }

        private static DbContextOptions GetOptions(string dbName)
        {
            string connectionString = Common.GetConnectionString(dbName);
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}

//Scaffold-DbContext "Server=NAT;Database=HTENTruckScaleDb_NAT;user=sa;password=123456;Trust Server Certificate=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir TruckModels -Context "TruckContext" -f -Table tblTicket,tblMaterial,tblProvider,tblUser