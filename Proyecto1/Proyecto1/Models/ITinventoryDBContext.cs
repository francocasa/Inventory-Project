using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Proyecto1.Models
{
    public partial class ITinventoryDBContext : DbContext
    {
        public ITinventoryDBContext()
        {
        }

        public ITinventoryDBContext(DbContextOptions<ITinventoryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accessory> Accessory { get; set; }
        public virtual DbSet<Cellphone> Cellphone { get; set; }
        public virtual DbSet<Dbmodel> Dbmodel { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Incidence> Incidence { get; set; }
        public virtual DbSet<Line> Line { get; set; }
        public virtual DbSet<LineCellphone> LineCellphone { get; set; }
        public virtual DbSet<LocationBuilding> LocationBuilding { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Projector> Projector { get; set; }
        public virtual DbSet<Radio> Radio { get; set; }
        public virtual DbSet<Screen> Screen { get; set; }
        public virtual DbSet<Software> Software { get; set; }
        public virtual DbSet<SoftwareEquipment> SoftwareEquipment { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserAccessory> UserAccessory { get; set; }
        public virtual DbSet<UserCellphone> UserCellphone { get; set; }
        public virtual DbSet<UserEquipment> UserEquipment { get; set; }
        public virtual DbSet<UserLine> UserLine { get; set; }
        public virtual DbSet<UserPhone> UserPhone { get; set; }
        public virtual DbSet<UserRadio> UserRadio { get; set; }
        public virtual DbSet<UserScreen> UserScreen { get; set; }
        public virtual DbSet<UserSoftware> UserSoftware { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=plngmelas05;initial catalog=ITinventoryDB;persist security info=True;user id=SWH_Gis;password=Pl@ntM3lchor1t@;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accessory>(entity =>
            {
                entity.HasKey(e => e.Identification);

                entity.Property(e => e.Identification)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment).HasMaxLength(50);

                entity.Property(e => e.Equipment)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Cellphone>(entity =>
            {
                entity.HasKey(e => e.Imei);

                entity.Property(e => e.Imei)
                    .HasColumnName("IMEI")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.AssignedDate).HasColumnType("date");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ExUser).HasMaxLength(50);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModelAirwatch).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.Observation).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Dbmodel>(entity =>
            {
                entity.ToTable("DBModel");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Characteristic)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameTable)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasKey(e => e.ServiceTag);

                entity.Property(e => e.ServiceTag)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.AssignedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Condition)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ExUser).HasMaxLength(50);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MainFunctionDevice)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NextRenewal)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PartOfLeasing)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.PurchaseDate).HasColumnType("date");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Storage).HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ValidatedOn).HasColumnType("date");

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.Location)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Equipment_LocationBuilding");
            });

            modelBuilder.Entity<Incidence>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IncidenceType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.UserNameNavigation)
                    .WithOne(p => p.Incidence)
                    .HasForeignKey<Incidence>(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Incidence_User");
            });

            modelBuilder.Entity<Line>(entity =>
            {
                entity.HasKey(e => e.Number);

                entity.Property(e => e.Number)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();

                entity.Property(e => e.BilledTo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContractExpiration).HasColumnType("date");

                entity.Property(e => e.LinePlan)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Supplier)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LineCellphone>(entity =>
            {
                entity.HasKey(e => new { e.Number, e.Imei });

                entity.Property(e => e.Number).HasMaxLength(20);

                entity.Property(e => e.Imei)
                    .HasColumnName("IMEI")
                    .HasMaxLength(50);

                entity.HasOne(d => d.ImeiNavigation)
                    .WithMany(p => p.LineCellphone)
                    .HasForeignKey(d => d.Imei)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LineCellphone_Cellphone");

                entity.HasOne(d => d.NumberNavigation)
                    .WithMany(p => p.LineCellphone)
                    .HasForeignKey(d => d.Number)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LineCellphone_Line");
            });

            modelBuilder.Entity<LocationBuilding>(entity =>
            {
                entity.HasKey(e => e.Location);

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Building)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasKey(e => e.Mac);

                entity.Property(e => e.Mac)
                    .HasColumnName("MAC")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Phone)
                    .HasForeignKey(d => d.Location)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Phone_LocationBuilding");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.HasKey(e => e.ProcessTitle);

                entity.Property(e => e.ProcessTitle)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.ProcessName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TableName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Projector>(entity =>
            {
                entity.HasKey(e => e.ServiceTag);

                entity.Property(e => e.ServiceTag)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PartOfLeasing)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.PurchaseDate).HasColumnType("date");

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Projector)
                    .HasForeignKey(d => d.Location)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Projector_LocationBuilding");
            });

            modelBuilder.Entity<Radio>(entity =>
            {
                entity.HasKey(e => e.ServiceTag);

                entity.Property(e => e.ServiceTag)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.LatestUpdate).HasColumnType("date");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Radio)
                    .HasForeignKey(d => d.Location)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Radio_LocationBuilding");
            });

            modelBuilder.Entity<Screen>(entity =>
            {
                entity.HasKey(e => e.ServiceTag);

                entity.Property(e => e.ServiceTag)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PartOfLeasing)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.PurchaseDate).HasColumnType("date");

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Screen)
                    .HasForeignKey(d => d.Location)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Screen_LocationBuilding1");
            });

            modelBuilder.Entity<Software>(entity =>
            {
                entity.HasKey(e => e.SoftwareCode);

                entity.Property(e => e.SoftwareCode)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.ContractFinal).HasColumnType("date");

                entity.Property(e => e.ContractStart).HasColumnType("date");

                entity.Property(e => e.Licensed)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.SoftwareName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SoftwareEquipment>(entity =>
            {
                entity.HasKey(e => new { e.SoftwareCode, e.ServiceTag });

                entity.Property(e => e.SoftwareCode).HasMaxLength(50);

                entity.Property(e => e.ServiceTag).HasMaxLength(50);

                entity.HasOne(d => d.ServiceTagNavigation)
                    .WithMany(p => p.SoftwareEquipment)
                    .HasForeignKey(d => d.ServiceTag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SoftwareEquipment_Equipment");

                entity.HasOne(d => d.SoftwareCodeNavigation)
                    .WithMany(p => p.SoftwareEquipment)
                    .HasForeignKey(d => d.SoftwareCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SoftwareEquipment_Software");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Annex).HasMaxLength(50);

                entity.Property(e => e.Deparment)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Supervisor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserAccessory>(entity =>
            {
                entity.HasKey(e => new { e.UserName, e.Identification });

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.Identification).HasMaxLength(50);

                entity.HasOne(d => d.IdentificationNavigation)
                    .WithMany(p => p.UserAccessory)
                    .HasForeignKey(d => d.Identification)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccesory_Accesory");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UserAccessory)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccesory_User");
            });

            modelBuilder.Entity<UserCellphone>(entity =>
            {
                entity.HasKey(e => new { e.UserName, e.Imei });

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.Imei)
                    .HasColumnName("IMEI")
                    .HasMaxLength(50);

                entity.HasOne(d => d.ImeiNavigation)
                    .WithMany(p => p.UserCellphone)
                    .HasForeignKey(d => d.Imei)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserCellphone_Cellphone");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UserCellphone)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserCellphone_User");
            });

            modelBuilder.Entity<UserEquipment>(entity =>
            {
                entity.HasKey(e => new { e.UserName, e.ServiceTag });

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.ServiceTag).HasMaxLength(50);

                entity.HasOne(d => d.ServiceTagNavigation)
                    .WithMany(p => p.UserEquipment)
                    .HasForeignKey(d => d.ServiceTag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserEquipment_Equipment");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UserEquipment)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserEquipment_User");
            });

            modelBuilder.Entity<UserLine>(entity =>
            {
                entity.HasKey(e => new { e.UserName, e.Number });

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(20);

                entity.HasOne(d => d.NumberNavigation)
                    .WithMany(p => p.UserLine)
                    .HasForeignKey(d => d.Number)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLine_Line");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UserLine)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLine_User");
            });

            modelBuilder.Entity<UserPhone>(entity =>
            {
                entity.HasKey(e => new { e.UserName, e.Mac });

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.Mac)
                    .HasColumnName("MAC")
                    .HasMaxLength(50);

                entity.HasOne(d => d.MacNavigation)
                    .WithMany(p => p.UserPhone)
                    .HasForeignKey(d => d.Mac)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPhone_Phone");

                entity.HasOne(d => d.Mac1)
                    .WithMany(p => p.UserPhone)
                    .HasForeignKey(d => d.Mac)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPhone_User");
            });

            modelBuilder.Entity<UserRadio>(entity =>
            {
                entity.HasKey(e => new { e.UserName, e.ServiceTag });

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.ServiceTag).HasMaxLength(50);

                entity.HasOne(d => d.ServiceTagNavigation)
                    .WithMany(p => p.UserRadio)
                    .HasForeignKey(d => d.ServiceTag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRadio_Radio");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UserRadio)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRadio_User");
            });

            modelBuilder.Entity<UserScreen>(entity =>
            {
                entity.HasKey(e => new { e.UserName, e.ServiceTag });

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.ServiceTag).HasMaxLength(50);

                entity.HasOne(d => d.ServiceTagNavigation)
                    .WithMany(p => p.UserScreen)
                    .HasForeignKey(d => d.ServiceTag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserScreen_Screen");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UserScreen)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserScreen_User");
            });

            modelBuilder.Entity<UserSoftware>(entity =>
            {
                entity.HasKey(e => new { e.UserName, e.SoftwareCode });

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.SoftwareCode).HasMaxLength(50);

                entity.HasOne(d => d.SoftwareCodeNavigation)
                    .WithMany(p => p.UserSoftware)
                    .HasForeignKey(d => d.SoftwareCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSoftware_Software1");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UserSoftware)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSoftware_User");
            });
        }
    }
}
