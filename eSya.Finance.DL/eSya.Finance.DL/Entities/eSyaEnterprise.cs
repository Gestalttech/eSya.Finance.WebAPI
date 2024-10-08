﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eSya.Finance.DL.Entities
{
    public partial class eSyaEnterprise : DbContext
    {
        public static string _connString = "";

        public eSyaEnterprise()
        {
        }

        public eSyaEnterprise(DbContextOptions<eSyaEnterprise> options)
            : base(options)
        {
        }

        public virtual DbSet<GtEcapcd> GtEcapcds { get; set; } = null!;
        public virtual DbSet<GtEcbsln> GtEcbslns { get; set; } = null!;
        public virtual DbSet<GtEccncd> GtEccncds { get; set; } = null!;
        public virtual DbSet<GtEccnpm> GtEccnpms { get; set; } = null!;
        public virtual DbSet<GtEccuco> GtEccucos { get; set; } = null!;
        public virtual DbSet<GtIfagdf> GtIfagdfs { get; set; } = null!;
        public virtual DbSet<GtIfaspg> GtIfaspgs { get; set; } = null!;
        public virtual DbSet<GtIfbtpm> GtIfbtpms { get; set; } = null!;
        public virtual DbSet<GtIfcocc> GtIfcoccs { get; set; } = null!;
        public virtual DbSet<GtIfcocl> GtIfcocls { get; set; } = null!;
        public virtual DbSet<GtIfcreh> GtIfcrehs { get; set; } = null!;
        public virtual DbSet<GtIfcrer> GtIfcrers { get; set; } = null!;
        public virtual DbSet<GtIffabt> GtIffabts { get; set; } = null!;
        public virtual DbSet<GtIfswm> GtIfswms { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(_connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GtEcapcd>(entity =>
            {
                entity.HasKey(e => e.ApplicationCode)
                    .HasName("PK_GT_ECAPCD_1");

                entity.ToTable("GT_ECAPCD");

                entity.Property(e => e.ApplicationCode).ValueGeneratedNever();

                entity.Property(e => e.CodeDesc).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ShortCode).HasMaxLength(15);
            });

            modelBuilder.Entity<GtEcbsln>(entity =>
            {
                entity.HasKey(e => new { e.BusinessId, e.LocationId });

                entity.ToTable("GT_ECBSLN");

                entity.HasIndex(e => e.BusinessKey, "IX_GT_ECBSLN")
                    .IsUnique();

                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.BusinessName).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.DateFormat).HasMaxLength(25);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.LocationDescription).HasMaxLength(150);

                entity.Property(e => e.Lstatus).HasColumnName("LStatus");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ShortDateFormat).HasMaxLength(15);

                entity.Property(e => e.ShortDesc).HasMaxLength(15);
            });

            modelBuilder.Entity<GtEccncd>(entity =>
            {
                entity.HasKey(e => e.Isdcode);

                entity.ToTable("GT_ECCNCD");

                entity.Property(e => e.Isdcode)
                    .ValueGeneratedNever()
                    .HasColumnName("ISDCode");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CountryFlag).HasMaxLength(150);

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.DateFormat).HasMaxLength(25);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.IsPinapplicable).HasColumnName("IsPINApplicable");

                entity.Property(e => e.IsPoboxApplicable).HasColumnName("IsPOBoxApplicable");

                entity.Property(e => e.MobileNumberPattern)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.PincodePattern)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PINcodePattern");

                entity.Property(e => e.PoboxPattern)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("POBoxPattern");

                entity.Property(e => e.ShortDateFormat).HasMaxLength(15);
            });

            modelBuilder.Entity<GtEccnpm>(entity =>
            {
                entity.HasKey(e => new { e.Isdcode, e.PaymentMethod, e.InstrumentType });

                entity.ToTable("GT_ECCNPM");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEccuco>(entity =>
            {
                entity.HasKey(e => e.CurrencyCode);

                entity.ToTable("GT_ECCUCO");

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.CurrencyName).HasMaxLength(25);

                entity.Property(e => e.DecimalPlaces).HasColumnType("decimal(6, 0)");

                entity.Property(e => e.DecimalPortionWord).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Symbol).HasMaxLength(10);
            });

            modelBuilder.Entity<GtIfagdf>(entity =>
            {
                entity.HasKey(e => e.GroupCode);

                entity.ToTable("GT_IFAGDF");

                entity.Property(e => e.GroupCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.BookType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CnControlAccount).HasColumnName("CN_ControlAccount");

                entity.Property(e => e.CnGeneralLedger).HasColumnName("CN_GeneralLedger");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.DnControlAccount).HasColumnName("DN_ControlAccount");

                entity.Property(e => e.DnGeneralLedger).HasColumnName("DN_GeneralLedger");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.GroupDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsIntegrateFa).HasColumnName("IsIntegrateFA");

                entity.Property(e => e.JControlAccount).HasColumnName("J_ControlAccount");

                entity.Property(e => e.JGeneralLedger).HasColumnName("J_GeneralLedger");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.NatureOfGroup)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PControlAccount).HasColumnName("P_ControlAccount");

                entity.Property(e => e.PGeneralLedger).HasColumnName("P_GeneralLedger");

                entity.Property(e => e.ParentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ParentID");

                entity.Property(e => e.PrControlAccount).HasColumnName("PR_ControlAccount");

                entity.Property(e => e.PrGeneralLedger).HasColumnName("PR_GeneralLedger");

                entity.Property(e => e.SControlAccount).HasColumnName("S_ControlAccount");

                entity.Property(e => e.SGeneralLedger).HasColumnName("S_GeneralLedger");
            });

            modelBuilder.Entity<GtIfaspg>(entity =>
            {
                entity.HasKey(e => e.AccountSgltype);

                entity.ToTable("GT_IFASPG");

                entity.Property(e => e.AccountSgltype)
                    .ValueGeneratedNever()
                    .HasColumnName("AccountSGLType");

                entity.Property(e => e.AccountSgldesc)
                    .HasMaxLength(50)
                    .HasColumnName("AccountSGLDesc");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtIfbtpm>(entity =>
            {
                entity.HasKey(e => new { e.BookType, e.VoucherType, e.InstrumentType });

                entity.ToTable("GT_IFBTPM");

                entity.Property(e => e.BookType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.VoucherType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.VoucherTypeDesc).HasMaxLength(25);
            });

            modelBuilder.Entity<GtIfcocc>(entity =>
            {
                entity.HasKey(e => e.CostCenterCode);

                entity.ToTable("GT_IFCOCC");

                entity.Property(e => e.CostCenterCode).ValueGeneratedNever();

                entity.Property(e => e.CostCenterDesc).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtIfcocl>(entity =>
            {
                entity.HasKey(e => e.CostCenterClass);

                entity.ToTable("GT_IFCOCL");

                entity.Property(e => e.CostCenterClass).ValueGeneratedNever();

                entity.Property(e => e.CostClassDesc).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtIfcreh>(entity =>
            {
                entity.HasKey(e => new { e.CountryCode, e.CurrencyCode });

                entity.ToTable("GT_IFCREH");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtIfcrer>(entity =>
            {
                entity.HasKey(e => new { e.CurrencyKey, e.DateOfExchangeRate })
                    .HasName("PK_GT_IFCRER_1");

                entity.ToTable("GT_IFCRER");

                entity.Property(e => e.DateOfExchangeRate).HasColumnType("datetime");

                entity.Property(e => e.BuyingLastVoucherDate).HasColumnType("datetime");

                entity.Property(e => e.BuyingRate).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.SellingLastVoucherDate).HasColumnType("datetime");

                entity.Property(e => e.SellingRate).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.StandardRate).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtIffabt>(entity =>
            {
                entity.HasKey(e => e.BookType);

                entity.ToTable("GT_IFFABT");

                entity.Property(e => e.BookType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.BookTypeDesc).HasMaxLength(25);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtIfswm>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.SwipingMachineId, e.ControlAccountCode })
                    .HasName("PK_GT_IFSWMS_1");

                entity.ToTable("GT_IFSWMS");

                entity.Property(e => e.SwipingMachineId)
                    .HasMaxLength(50)
                    .HasColumnName("SwipingMachineID");

                entity.Property(e => e.ControlAccountCode).HasMaxLength(15);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .HasColumnName("FormID")
                    .IsFixedLength();

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.SwipingMachineName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
