using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HtenTrobzApi.TruckModels
{
    public partial class TruckContext : DbContext
    {
        public virtual DbSet<TblMaterial> TblMaterials { get; set; } = null!;
        public virtual DbSet<TblProvider> TblProviders { get; set; } = null!;
        public virtual DbSet<TblTicket> TblTickets { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblMaterial>(entity =>
            {
                entity.ToTable("tblMaterial");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.AdditionFee)
                    .HasColumnName("Addition_Fee")
                    .HasComment("Chi phi khac");

                entity.Property(e => e.BarCode)
                    .HasMaxLength(50)
                    .HasComment("Ma vach");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreateLog)
                    .HasMaxLength(50)
                    .HasColumnName("Create_Log");

                entity.Property(e => e.Currency)
                    .HasMaxLength(20)
                    .HasComment("Don vi tien te");

                entity.Property(e => e.HotKey).HasMaxLength(50);

                entity.Property(e => e.LastModifyLog)
                    .HasMaxLength(50)
                    .HasColumnName("LastModify_Log");

                entity.Property(e => e.LastSynchTime).HasColumnType("datetime");

                entity.Property(e => e.MatGroup).HasMaxLength(20);

                entity.Property(e => e.MaterialUnit)
                    .HasMaxLength(50)
                    .HasColumnName("Material_Unit")
                    .HasComment("Don vi tinh cua nguyen lieu");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasComment("Ten nguyen lieu");

                entity.Property(e => e.Note)
                    .HasMaxLength(100)
                    .HasComment("Ghi chu");

                entity.Property(e => e.Origin)
                    .HasMaxLength(50)
                    .HasComment("Xuat xu");

                entity.Property(e => e.Syn)
                    .HasMaxLength(50)
                    .HasColumnName("SYN");

                entity.Property(e => e.UnitPrice)
                    .HasColumnName("Unit_Price")
                    .HasComment("Don gia nguyen lieu");

                entity.Property(e => e.UserChanged).HasMaxLength(50);

                entity.Property(e => e.UserCreated).HasMaxLength(50);
            });

            modelBuilder.Entity<TblProvider>(entity =>
            {
                entity.ToTable("tblProvider");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Contact).HasMaxLength(50);

                entity.Property(e => e.CreateLog)
                    .HasMaxLength(30)
                    .HasColumnName("Create_Log");

                entity.Property(e => e.HotKey).HasMaxLength(50);

                entity.Property(e => e.LastModifyLog)
                    .HasMaxLength(30)
                    .HasColumnName("LastModify_Log");

                entity.Property(e => e.LastSynchTime).HasColumnType("datetime");

                entity.Property(e => e.Logo).HasColumnType("image");

                entity.Property(e => e.Mail).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Note)
                    .HasMaxLength(200)
                    .HasComment("Ghi chu");

                entity.Property(e => e.Syn)
                    .HasMaxLength(50)
                    .HasColumnName("SYN");

                entity.Property(e => e.UserChanged).HasMaxLength(50);

                entity.Property(e => e.UserCreated).HasMaxLength(50);
            });

            modelBuilder.Entity<TblTicket>(entity =>
            {
                entity.ToTable("tblTicket");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DriverName).HasMaxLength(200);

                entity.Property(e => e.GrossDatetime).HasColumnType("datetime");

                entity.Property(e => e.KepChi).HasMaxLength(50);

                entity.Property(e => e.LastSynchTime).HasColumnType("datetime");

                entity.Property(e => e.MatGroup).HasMaxLength(20);

                entity.Property(e => e.PlantNoCbp).HasColumnName("Plant_NoCBP");

                entity.Property(e => e.SealNoCbp)
                    .HasMaxLength(20)
                    .HasColumnName("Seal_NoCBP");

                entity.Property(e => e.SheetNoCbp).HasColumnName("Sheet_NoCBP");

                entity.Property(e => e.StationId)
                    .HasMaxLength(50)
                    .HasColumnName("STATION_ID");

                entity.Property(e => e.Syn)
                    .HasMaxLength(50)
                    .HasColumnName("SYN");

                entity.Property(e => e.Sync)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TareDatetime).HasColumnType("datetime");

                entity.Property(e => e.TruckPlateNumber).HasMaxLength(50);

                entity.HasOne(d => d.GrossOperConfirmNavigation)
                    .WithMany(p => p.TblTicketGrossOperConfirmNavigations)
                    .HasForeignKey(d => d.GrossOperConfirm)
                    .HasConstraintName("FK_tblTicket_tblUser");

                entity.HasOne(d => d.MaterialRefNavigation)
                    .WithMany(p => p.TblTickets)
                    .HasForeignKey(d => d.MaterialRef)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblTicket_tblMaterial");

                entity.HasOne(d => d.ProviderRefNavigation)
                    .WithMany(p => p.TblTickets)
                    .HasForeignKey(d => d.ProviderRef)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblTicket_tblProvider");

                entity.HasOne(d => d.TareOperConfirmNavigation)
                    .WithMany(p => p.TblTicketTareOperConfirmNavigations)
                    .HasForeignKey(d => d.TareOperConfirm)
                    .HasConstraintName("FK_tblTicket_tblUser1");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("tblUser");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ActiveEncypt).HasMaxLength(100);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Contact).HasMaxLength(50);

                entity.Property(e => e.CreateLog)
                    .HasMaxLength(30)
                    .HasColumnName("Create_Log");

                entity.Property(e => e.DepartmentName).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifyLog)
                    .HasMaxLength(30)
                    .HasColumnName("LastModify_Log");

                entity.Property(e => e.LastSynchTime).HasColumnType("datetime");

                entity.Property(e => e.Mail).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.StationId)
                    .HasMaxLength(50)
                    .HasColumnName("STATION_ID");

                entity.Property(e => e.Syn)
                    .HasMaxLength(50)
                    .HasColumnName("SYN");

                entity.Property(e => e.UserChanged).HasMaxLength(50);

                entity.Property(e => e.UserCreated).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
