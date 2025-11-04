using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lotus_Dashboard1.Apis.GoldEtemadContext
{
    public partial class LotusibBIContext : DbContext
    {
        public LotusibBIContext()
        {
        }

        public LotusibBIContext(DbContextOptions<LotusibBIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GoldEtemad> GoldEtemads { get; set; } = null!;
        public virtual DbSet<RevokeQueue> RevokeQueues { get; set; } = null!;
        public virtual DbSet<TCustomer> TCustomers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=192.168.1.131;Initial Catalog=LotusibBI;User ID=balavand;Password=123456;Encrypt=False;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoldEtemad>(entity =>
            {
                entity.ToTable("Gold_Etemad");

                entity.Property(e => e.BankAccountId)
                    .HasMaxLength(100)
                    .HasColumnName("bankAccountId");

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(100)
                    .HasColumnName("branchCode");

                entity.Property(e => e.CardNumber).HasMaxLength(100);

                entity.Property(e => e.DsName)
                    .HasMaxLength(30)
                    .HasColumnName("dsName");

                entity.Property(e => e.FundIssueTypeId)
                    .HasMaxLength(10)
                    .HasColumnName("fundIssueTypeId");

                entity.Property(e => e.FundOrderId)
                    .HasMaxLength(10)
                    .HasColumnName("fundOrderId");

                entity.Property(e => e.NationalCode)
                    .HasMaxLength(15)
                    .HasColumnName("national_code");

                entity.Property(e => e.OrderAmount).HasColumnName("orderAmount");

                entity.Property(e => e.OrderPaymentTypeId)
                    .HasMaxLength(10)
                    .HasColumnName("orderPaymentTypeId");

                entity.Property(e => e.ReceiptComments)
                    .HasMaxLength(20)
                    .HasColumnName("receiptComments");

                entity.Property(e => e.ReceiptDate)
                    .HasMaxLength(15)
                    .HasColumnName("receiptDate");

                entity.Property(e => e.ReceiptNumber)
                    .HasMaxLength(15)
                    .HasColumnName("receiptNumber");

                entity.Property(e => e.ReceiptTime)
                    .HasMaxLength(20)
                    .HasColumnName("receiptTime");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<RevokeQueue>(entity =>
            {
                entity.ToTable("Revoke_Queue");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DsName)
                    .HasMaxLength(10)
                    .HasColumnName("dsName");

                entity.Property(e => e.FundOrderId).HasColumnName("fundOrderId");

                entity.Property(e => e.FundOrderNumber)
                    .HasMaxLength(20)
                    .HasColumnName("fundOrderNumber");

                entity.Property(e => e.FundUnit).HasColumnName("fundUnit");

                entity.Property(e => e.LicenseNumber)
                    .HasMaxLength(20)
                    .HasColumnName("licenseNumber");

                entity.Property(e => e.NationalCode)
                    .HasMaxLength(20)
                    .HasColumnName("national_code");

                entity.Property(e => e.OrderDate)
                    .HasMaxLength(15)
                    .HasColumnName("orderDate");

                entity.Property(e => e.OrderTime)
                    .HasMaxLength(30)
                    .HasColumnName("orderTime");

                entity.Property(e => e.Title)
                    .HasMaxLength(10)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<TCustomer>(entity =>
            {
                entity.HasKey(e => e.TCustomerSk);

                entity.ToTable("T_CUSTOMER");

                entity.Property(e => e.TCustomerSk).HasColumnName("T_Customer_SK");

                entity.Property(e => e.Address)
                    .HasMaxLength(1000)
                    .HasColumnName("ADDRESS")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.BankAccountId).HasColumnName("BANK_ACCOUNT_ID");

                entity.Property(e => e.BirthCertificationBigint)
                    .HasMaxLength(50)
                    .HasColumnName("BIRTH_CERTIFICATION_BIGINT")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.BirthCertificationId)
                    .HasMaxLength(30)
                    .HasColumnName("BIRTH_CERTIFICATION_ID")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.BirthDate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("BIRTH_DATE")
                    .IsFixedLength()
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.BourseAccountId).HasColumnName("BOURSE_ACCOUNT_ID");

                entity.Property(e => e.BranchId).HasColumnName("BRANCH_ID");

                entity.Property(e => e.CellPhone)
                    .HasMaxLength(100)
                    .HasColumnName("CELL_PHONE")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(200)
                    .HasColumnName("COMPANY_NAME")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CompanyNationalCode)
                    .HasMaxLength(11)
                    .HasColumnName("COMPANY_NATIONAL_CODE")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CreationDate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_DATE")
                    .IsFixedLength()
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CreationTime)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_TIME")
                    .IsFixedLength()
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.CustomerStatusId).HasColumnName("CUSTOMER_STATUS_ID");

                entity.Property(e => e.DlId).HasColumnName("DL_ID");

                entity.Property(e => e.EMail)
                    .HasMaxLength(100)
                    .HasColumnName("E_MAIL")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Fax)
                    .HasMaxLength(100)
                    .HasColumnName("FAX")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("FIRST_NAME")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.IsCompany).HasColumnName("IS_COMPANY");

                entity.Property(e => e.IsIranian)
                    .HasColumnName("IS_IRANIAN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsProfitIssue).HasColumnName("IS_PROFIT_ISSUE");

                entity.Property(e => e.IsValidEmailAddress).HasColumnName("IS_VALID_EMAIL_ADDRESS");

                entity.Property(e => e.IsValidMobileBigint).HasColumnName("IS_VALID_MOBILE_BIGINT");

                entity.Property(e => e.IsValidNationalCode).HasColumnName("IS_VALID_NATIONAL_CODE");

                entity.Property(e => e.IssuingCity)
                    .HasMaxLength(100)
                    .HasColumnName("ISSUING_CITY")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .HasColumnName("LAST_NAME")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ModificationDate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MODIFICATION_DATE")
                    .IsFixedLength()
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ModificationTime)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MODIFICATION_TIME")
                    .IsFixedLength()
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.NationalCode)
                    .HasMaxLength(20)
                    .HasColumnName("NATIONAL_CODE")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Parent)
                    .HasMaxLength(200)
                    .HasColumnName("PARENT")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Phone)
                    .HasMaxLength(200)
                    .HasColumnName("PHONE")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.PortfolioStartDate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PORTFOLIO_START_DATE")
                    .IsFixedLength()
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(20)
                    .HasColumnName("POSTAL_CODE")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.RegistrationDate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("REGISTRATION_DATE")
                    .IsFixedLength()
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.WebSite)
                    .HasMaxLength(100)
                    .HasColumnName("WEB_SITE")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            OnModelCreatingPartial(modelBuilder);
           
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
