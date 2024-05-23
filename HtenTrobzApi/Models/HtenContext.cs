using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HtenTrobzApi.Models
{
    public partial class HtenContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Driver> Drivers { get; set; } = null!;
        public virtual DbSet<FastErrorLog> FastErrorLogs { get; set; } = null!;
        public virtual DbSet<H00member> H00members { get; set; } = null!;
        public virtual DbSet<MaterialArch> MaterialArches { get; set; } = null!;
        public virtual DbSet<MaterialList> MaterialLists { get; set; } = null!;
        public virtual DbSet<SaleContract> SaleContracts { get; set; } = null!;
        public virtual DbSet<SaleContractDetail> SaleContractDetails { get; set; } = null!;
        public virtual DbSet<Site> Sites { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Truck> Trucks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("Customer");

                entity.HasIndex(e => e.Code, "IX_Customer");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.AddressLine1).HasMaxLength(255);

                entity.Property(e => e.AddressLine2).HasMaxLength(255);

                entity.Property(e => e.AddressLine3).HasMaxLength(255);

                entity.Property(e => e.ContactMan).HasMaxLength(100);

                entity.Property(e => e.CreateLog)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Log");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.LastModifyLog)
                    .HasColumnType("datetime")
                    .HasColumnName("LastModify_Log");

                entity.Property(e => e.Locality).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.Property(e => e.Rv)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("RV");

                entity.Property(e => e.SaleMan).HasMaxLength(50);

                entity.Property(e => e.Telephone).HasMaxLength(50);

                entity.Property(e => e.UserChange).HasMaxLength(50);

                entity.Property(e => e.UserCreate).HasMaxLength(50);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CodePlant).HasMaxLength(50);

                entity.Property(e => e.CreateLog)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Log");

                entity.Property(e => e.KmCovered).HasColumnName("Km_Covered");

                entity.Property(e => e.LastModifyLog)
                    .HasColumnType("datetime")
                    .HasColumnName("LastModify_Log");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Rv)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("RV");

                entity.Property(e => e.TripsNumber).HasColumnName("Trips_Number");

                entity.Property(e => e.UserChange).HasMaxLength(50);

                entity.Property(e => e.UserCreate).HasMaxLength(50);
            });

            modelBuilder.Entity<FastErrorLog>(entity =>
            {
                entity.ToTable("FastErrorLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<H00member>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK__H00MEMBE__42A68F27748E1482");

                entity.ToTable("H00MEMBER");

                entity.Property(e => e.MemberId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Member_ID");

                entity.Property(e => e.IsAdmin).HasColumnName("Is_Admin");

                entity.Property(e => e.IsMemberId)
                    .IsRequired()
                    .HasColumnName("Is_MemberID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MaCbNv)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Ma_CbNv")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MaDvCsDefault)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("Ma_DvCs_Default")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MemberIdAllow)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Member_ID_Allow")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MemberName)
                    .HasMaxLength(100)
                    .HasColumnName("Member_Name")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MemberType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Member_Type")
                    .HasDefaultValueSql("('U')")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PlantList)
                    .HasMaxLength(2000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MaterialArch>(entity =>
            {
                entity.ToTable("Material_Arch");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Abmoist).HasColumnName("ABMoist");

                entity.Property(e => e.BatchId).HasColumnName("Batch_ID");

                entity.Property(e => e.CodeMaterial)
                    .HasMaxLength(20)
                    .HasColumnName("Code_Material");

                entity.Property(e => e.CodePlant).HasMaxLength(50);

                entity.Property(e => e.ComNum).HasColumnName("COM_Num");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_End");

                entity.Property(e => e.DecimalNo).HasColumnName("Decimal_No");

                entity.Property(e => e.ModeRun)
                    .HasMaxLength(20)
                    .HasColumnName("Mode_Run");

                entity.Property(e => e.NameMaterial)
                    .HasMaxLength(50)
                    .HasColumnName("Name_Material");

                entity.Property(e => e.PlantNo).HasColumnName("Plant_No");

                entity.Property(e => e.PvActualy).HasColumnName("PV_Actualy");

                entity.Property(e => e.RecipeId)
                    .HasMaxLength(50)
                    .HasColumnName("Recipe_ID");

                entity.Property(e => e.SheetId).HasColumnName("Sheet_ID");

                entity.Property(e => e.Sim).HasColumnName("SIM");

                entity.Property(e => e.SpRecipe).HasColumnName("SP_Recipe");

                entity.Property(e => e.SpTarget).HasColumnName("SP_Target");

                entity.Property(e => e.Sync)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sync")
                    .IsFixedLength();

                entity.Property(e => e.TimeEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("Time_End");

                entity.Property(e => e.UnitMaterial)
                    .HasMaxLength(20)
                    .HasColumnName("Unit_Material");
            });

            modelBuilder.Entity<MaterialList>(entity =>
            {
                entity.HasKey(e => e.ComName);

                entity.ToTable("MaterialList");

                entity.Property(e => e.ComName)
                    .HasMaxLength(20)
                    .HasColumnName("Com_Name");

                entity.Property(e => e.Cot).HasMaxLength(20);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Rv)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("RV");

                entity.Property(e => e.Sync)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sync")
                    .IsFixedLength();

                entity.Property(e => e.TypeMaterial).HasMaxLength(20);

                entity.Property(e => e.Unit).HasMaxLength(20);
            });

            modelBuilder.Entity<SaleContract>(entity =>
            {
                entity.ToTable("SaleContract");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(30);

                entity.Property(e => e.CreateLog)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Log");

                entity.Property(e => e.CustomerCode)
                    .HasMaxLength(30)
                    .HasColumnName("Customer_Code");

                entity.Property(e => e.Description01).HasMaxLength(100);

                entity.Property(e => e.Description02).HasMaxLength(100);

                entity.Property(e => e.EmployeeCode).HasMaxLength(20);

                entity.Property(e => e.LastModifyLog)
                    .HasColumnType("datetime")
                    .HasColumnName("LastModify_Log");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.SiteCode)
                    .HasMaxLength(30)
                    .HasColumnName("Site_Code");

                entity.Property(e => e.UserChange).HasMaxLength(50);

                entity.Property(e => e.UserCreate).HasMaxLength(50);
            });

            modelBuilder.Entity<SaleContractDetail>(entity =>
            {
                entity.ToTable("SaleContractDetail");

                entity.Property(e => e.Description01).HasMaxLength(50);

                entity.Property(e => e.GradeSaleCode).HasMaxLength(50);

                entity.Property(e => e.NotesProduct).HasMaxLength(50);

                entity.Property(e => e.OrderedM3).HasColumnName("Ordered_M3");

                entity.Property(e => e.SaleContractCode).HasMaxLength(30);
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("Site");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.AddressLine1).HasMaxLength(255);

                entity.Property(e => e.AddressLine2).HasMaxLength(255);

                entity.Property(e => e.AddressLine3).HasMaxLength(255);

                entity.Property(e => e.ContactMan).HasMaxLength(100);

                entity.Property(e => e.CreateLog)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Log");

                entity.Property(e => e.LastModifyLog)
                    .HasColumnType("datetime")
                    .HasColumnName("LastModify_Log");

                entity.Property(e => e.Locality).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Rv)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("RV");

                entity.Property(e => e.Telephone).HasMaxLength(50);

                entity.Property(e => e.TransportTime).HasColumnName("Transport_time");

                entity.Property(e => e.UserChange).HasMaxLength(50);

                entity.Property(e => e.UserCreate).HasMaxLength(50);

                entity.Property(e => e.Zone).HasMaxLength(50);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.Idticket);

                entity.ToTable("Ticket");

                entity.Property(e => e.Idticket).HasColumnName("IDTicket");

                entity.Property(e => e.AdditivesCode).HasMaxLength(50);

                entity.Property(e => e.AddressCustomer)
                    .HasMaxLength(255)
                    .HasColumnName("Address_Customer");

                entity.Property(e => e.AddressSite)
                    .HasMaxLength(255)
                    .HasColumnName("Address_Site");

                entity.Property(e => e.BatchTotal).HasColumnName("Batch_Total");

                entity.Property(e => e.CodePlant).HasMaxLength(50);

                entity.Property(e => e.CusCommentAtSite)
                    .HasMaxLength(200)
                    .HasColumnName("Cus_CommentAtSite");

                entity.Property(e => e.Customer).HasMaxLength(255);

                entity.Property(e => e.CustomerCode)
                    .HasMaxLength(50)
                    .HasColumnName("Customer_Code");

                entity.Property(e => e.DateTimeDisSite)
                    .HasColumnType("datetime")
                    .HasColumnName("DateTime_DisSite");

                entity.Property(e => e.DateTimeEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("DateTime_End");

                entity.Property(e => e.DateTimeEndSite)
                    .HasColumnType("datetime")
                    .HasColumnName("DateTime_EndSite");

                entity.Property(e => e.DateTimeMix)
                    .HasColumnType("datetime")
                    .HasColumnName("DateTime_Mix");

                entity.Property(e => e.DateTimePrint)
                    .HasColumnType("datetime")
                    .HasColumnName("DateTime_Print");

                entity.Property(e => e.DeliveryNoOrder)
                    .HasMaxLength(20)
                    .HasColumnName("Delivery_No_Order");

                entity.Property(e => e.Driver).HasMaxLength(50);

                entity.Property(e => e.DriverCode)
                    .HasMaxLength(20)
                    .HasColumnName("Driver_Code");

                entity.Property(e => e.History).HasMaxLength(200);

                entity.Property(e => e.IntensityCode).HasMaxLength(50);

                entity.Property(e => e.M3Balance).HasColumnName("m3_Balance");

                entity.Property(e => e.M3Delivered).HasColumnName("m3_Delivered");

                entity.Property(e => e.M3Ordered).HasColumnName("m3_Ordered");

                entity.Property(e => e.M3PrintTicket).HasColumnName("m3_PrintTicket");

                entity.Property(e => e.M3Reject).HasColumnName("m3_Reject");

                entity.Property(e => e.M3ThisOrder).HasColumnName("m3_ThisOrder");

                entity.Property(e => e.M3ThisTicket).HasColumnName("m3_ThisTicket");

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.Property(e => e.Operator).HasMaxLength(50);

                entity.Property(e => e.OrderDescription01)
                    .HasMaxLength(255)
                    .HasColumnName("Order_Description01");

                entity.Property(e => e.OrderDescription02)
                    .HasMaxLength(255)
                    .HasColumnName("Order_Description02");

                entity.Property(e => e.OrderNo)
                    .HasMaxLength(50)
                    .HasColumnName("Order_No");

                entity.Property(e => e.PlantNo).HasColumnName("Plant_No");

                entity.Property(e => e.PumpCode).HasMaxLength(50);

                entity.Property(e => e.RatioWcactual).HasColumnName("RatioWCActual");

                entity.Property(e => e.RatioWctarget).HasColumnName("RatioWCTarget");

                entity.Property(e => e.RcDescription01)
                    .HasMaxLength(50)
                    .HasColumnName("RC_Description01");

                entity.Property(e => e.RcDescription02)
                    .HasMaxLength(50)
                    .HasColumnName("RC_Description02");

                entity.Property(e => e.RcSlump)
                    .HasMaxLength(50)
                    .HasColumnName("RC_Slump");

                entity.Property(e => e.RcType)
                    .HasMaxLength(50)
                    .HasColumnName("RC_Type");

                entity.Property(e => e.RecipeCode)
                    .HasMaxLength(50)
                    .HasColumnName("Recipe_Code");

                entity.Property(e => e.SealNo)
                    .HasMaxLength(20)
                    .HasColumnName("Seal_No");

                entity.Property(e => e.SheetNo).HasColumnName("Sheet_No");

                entity.Property(e => e.Sim).HasColumnName("SIM");

                entity.Property(e => e.Site).HasMaxLength(255);

                entity.Property(e => e.SiteCode)
                    .HasMaxLength(50)
                    .HasColumnName("Site_Code");

                entity.Property(e => e.SlumpAtSite).HasMaxLength(50);

                entity.Property(e => e.Sync)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TicketNo)
                    .HasMaxLength(20)
                    .HasColumnName("Ticket_No");

                entity.Property(e => e.Truck).HasMaxLength(50);

                entity.Property(e => e.TruckCode)
                    .HasMaxLength(20)
                    .HasColumnName("Truck_Code");

                entity.Property(e => e.WcRatio)
                    .HasMaxLength(50)
                    .HasColumnName("WC Ratio");
            });

            modelBuilder.Entity<Truck>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("Truck");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CodePlant).HasMaxLength(50);

                entity.Property(e => e.CreateLog)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Log");

                entity.Property(e => e.Dmnlcode)
                    .HasMaxLength(50)
                    .HasColumnName("DMNLCode");

                entity.Property(e => e.DriverCode).HasMaxLength(20);

                entity.Property(e => e.KmCovered).HasColumnName("Km_Covered");

                entity.Property(e => e.LastModifyLog)
                    .HasColumnType("datetime")
                    .HasColumnName("LastModify_Log");

                entity.Property(e => e.M3Transported).HasColumnName("M3_Transported");

                entity.Property(e => e.MinimumCharging).HasColumnName("Minimum_charging");

                entity.Property(e => e.PlateNumber)
                    .HasMaxLength(50)
                    .HasColumnName("Plate_Number");

                entity.Property(e => e.Rv)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("RV");

                entity.Property(e => e.Sddk).HasColumnName("SDDK");

                entity.Property(e => e.UserChange).HasMaxLength(50);

                entity.Property(e => e.UserCreate).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
