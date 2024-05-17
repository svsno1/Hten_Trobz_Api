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
        public virtual DbSet<H00member> H00members { get; set; } = null!;
        public virtual DbSet<MaterialList> MaterialLists { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Site> Sites { get; set; } = null!;
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

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BizdocType).HasMaxLength(1);

                entity.Property(e => e.Category).HasMaxLength(100);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CodePlant).HasMaxLength(50);

                entity.Property(e => e.Contact).HasMaxLength(50);

                entity.Property(e => e.CreateLog)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Log");

                entity.Property(e => e.CustomerCode)
                    .HasMaxLength(50)
                    .HasColumnName("Customer_Code");

                entity.Property(e => e.Description01).HasMaxLength(255);

                entity.Property(e => e.Description02).HasMaxLength(255);

                entity.Property(e => e.EmployeeCode).HasMaxLength(20);

                entity.Property(e => e.LastModifyLog)
                    .HasColumnType("datetime")
                    .HasColumnName("LastModify_Log");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.PlantNo).HasColumnName("Plant_No");

                entity.Property(e => e.SiteCode)
                    .HasMaxLength(50)
                    .HasColumnName("Site_Code");

                entity.Property(e => e.Sync)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sync")
                    .IsFixedLength();

                entity.Property(e => e.UserChange).HasMaxLength(50);

                entity.Property(e => e.UserCreate).HasMaxLength(50);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.AdditivesCode).HasMaxLength(50);

                entity.Property(e => e.CodePlant).HasMaxLength(50);

                entity.Property(e => e.Description01).HasMaxLength(50);

                entity.Property(e => e.IntensityCode).HasMaxLength(50);

                entity.Property(e => e.NotesProduct).HasMaxLength(255);

                entity.Property(e => e.OrderCode).HasMaxLength(50);

                entity.Property(e => e.OrderedM3).HasColumnName("Ordered_M3");

                entity.Property(e => e.PlantNo).HasColumnName("Plant_No");

                entity.Property(e => e.ProductedM3).HasColumnName("Producted_M3");

                entity.Property(e => e.PumpList)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.RecipeCode).HasMaxLength(50);

                entity.Property(e => e.RowId).HasMaxLength(16);

                entity.Property(e => e.SlumpCode).HasMaxLength(50);

                entity.Property(e => e.Sync)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sync")
                    .IsFixedLength();
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
