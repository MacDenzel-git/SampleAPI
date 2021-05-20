using System;
using DataAccessLayer.Authentication;
using DataAccessLayer.Authentication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class ProjectWebsiteDBContext : IdentityDbContext<User, Role, Guid>
    {
        public ProjectWebsiteDBContext()
        {
        }

        public ProjectWebsiteDBContext(DbContextOptions<ProjectWebsiteDBContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<ProjectPartnershipPlatform> GivingPlatforms { get; set; }
        public virtual DbSet<projectArm> projectArms { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Qoute> Qoutes { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<ResourceCategory> ResourceCategories { get; set; }
        public virtual DbSet<ResourceType> ResourceTypes { get; set; }
        public virtual DbSet<TeamMember> TeamMembers { get; set; }
        public virtual DbSet<Testimony> Testimonies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=projectWebsiteDB; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branch");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.IsPublished).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Branch_Districts1");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DistrictName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Districts_Countries");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Article).IsRequired();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfEvent).HasColumnType("datetime");

                entity.Property(e => e.EventTitle).IsRequired();

                entity.Property(e => e.Heading).IsRequired();

                entity.Property(e => e.ImageUrl).IsRequired();

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.projectArm)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.projectArmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_projectArms");
            });

            modelBuilder.Entity<ProjectPartnershipPlatform>(entity =>
            {
                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Platform)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.GivingPlatforms)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GivingPlatforms_Branch");
            });

            modelBuilder.Entity<projectArm>(entity =>
            {
                entity.Property(e => e.Artwork).IsRequired();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.projectArmAbbreviation)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.projectArmName).IsRequired();

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PositionName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Qoute>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasMaxLength(50);

                entity.Property(e => e.QouteText).IsRequired();
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("Resource");

                entity.Property(e => e.ResourceId).HasColumnName("ResourceID");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Preacher)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ResourceCategoryId).HasColumnName("ResourceCategoryID");

                entity.Property(e => e.ResourceName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.ResourceCategory)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.ResourceCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Resource_ResourceCategory");

                entity.HasOne(d => d.ResourceType)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.ResourceTypeId)
                    .HasConstraintName("FK_Resource_Resource");
            });

            modelBuilder.Entity<ResourceCategory>(entity =>
            {
                entity.ToTable("ResourceCategory");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ResourceType>(entity =>
            {
                entity.ToTable("ResourceType");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ResourceTypeName).IsRequired();
            });

            modelBuilder.Entity<TeamMember>(entity =>
            {
                entity.Property(e => e.TeamMemberId).HasColumnName("TeamMemberID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsEcmember).HasColumnName("IsECMember");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.TeamMembers)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamMembers_Branch");

                entity.HasOne(d => d.projectArm)
                    .WithMany(p => p.TeamMembers)
                    .HasForeignKey(d => d.projectArmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamMembers_projectArms");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.TeamMembers)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamMembers_Positions");
            });

            modelBuilder.Entity<Testimony>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ImageUrl).IsRequired();

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TestifierName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TestimonyDate).HasColumnType("datetime");

                entity.Property(e => e.TestimonyFullDescription).IsRequired();

                entity.Property(e => e.TestimonyHeading).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
