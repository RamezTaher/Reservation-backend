using Zamazimah.Data.DatabaseConfiguration;
using Zamazimah.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zamazimah.Entities;

namespace Zamazimah.Data.Context
{
	public partial class ZamazimahContext : IdentityDbContext<ApplicationUser>
	{
		public ZamazimahContext(DbContextOptions<ZamazimahContext> options) : base(options)
		{

		}
		// ----------- identity -------------------------------------------------------
		public virtual DbSet<Role> ApplicationRoles { get; set; }
		public virtual DbSet<Permission> Permissions { get; set; }
		public virtual DbSet<Group> Groups { get; set; }
		public virtual DbSet<Zone> Zones { get; set; }
		public virtual DbSet<GroupRole> GroupRoles { get; set; }
		public virtual DbSet<PermissionRole> PermissionRoles { get; set; }
		public virtual DbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }
		public virtual DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
		// ----------- identity -------------------------------------------------------
		public virtual DbSet<HousingContract> HousingContracts { get; set; }
		public virtual DbSet<PilgrimsTrip> PilgrimsTrips { get; set; }
		public virtual DbSet<Vehicle> Vehicles { get; set; }
		public virtual DbSet<DistributionContract> DistributionContracts { get; set; }
		public virtual DbSet<ReportType> ReportTypes { get; set; }
		public virtual DbSet<Report> Reports { get; set; }
		public virtual DbSet<Store> Stores { get; set; }
		public virtual DbSet<DistributionCycle> DistributionCycles { get; set; }
		public virtual DbSet<DistributionCycleHousingContract> DistributionCycleHousingContracts { get; set; }
		public virtual DbSet<Attachement> Attachements { get; set; }
		public virtual DbSet<LocationNature> LocationNatures { get; set; }
		public virtual DbSet<DistributionPoint> DistributionPoints { get; set; }
		public virtual DbSet<City> Cities { get; set; }
		public virtual DbSet<DistributorInventory> DistributorInventories { get; set; }
		public virtual DbSet<TransportCompany> TransportCompanies { get; set; }
		public virtual DbSet<Center> Centers { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// ----------- identity -------------------------------------------------------
			modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id);
			modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserPermissions");
			modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
			modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
			modelBuilder.Entity<IdentityRole>().ToTable("Permissions");

			modelBuilder.Entity<GroupRole>()
				 .HasKey(gr => new { gr.GroupId, gr.RoleId });
			modelBuilder.Entity<GroupRole>()
				 .HasOne(bc => bc.Role)
				 .WithMany(b => b.GroupRoles)
				 .HasForeignKey(bc => bc.RoleId);
			modelBuilder.Entity<GroupRole>()
				 .HasOne(bc => bc.Group)
				 .WithMany(c => c.GroupRoles)
				 .HasForeignKey(bc => bc.GroupId);

			modelBuilder.Entity<PermissionRole>()
				 .HasKey(gr => new { gr.PermissionId, gr.RoleId });
			modelBuilder.Entity<PermissionRole>()
				 .HasOne(bc => bc.Role)
				 .WithMany(b => b.PermissionRoles)
				 .HasForeignKey(bc => bc.RoleId);
			modelBuilder.Entity<PermissionRole>()
				 .HasOne(bc => bc.Permission)
				 .WithMany(c => c.PermissionRoles)
				 .HasForeignKey(bc => bc.PermissionId);


			modelBuilder.Entity<ApplicationUserRole>()
			.HasKey(gr => new { gr.ApplicationUserId, gr.RoleId });

			modelBuilder.Entity<ApplicationUserRole>()
				 .HasOne(bc => bc.Role)
				 .WithMany(b => b.ApplicationUserRoles)
				 .HasForeignKey(bc => bc.RoleId);

			modelBuilder.Entity<ApplicationUserRole>()
				 .HasOne(bc => bc.ApplicationUser)
				 .WithMany(c => c.ApplicationUserRoles)
				 .HasForeignKey(bc => bc.ApplicationUserId);


			modelBuilder.Entity<ApplicationUserGroup>()
				 .HasKey(gr => new { gr.GroupId, gr.ApplicationUserId });
			modelBuilder.Entity<ApplicationUserGroup>()
				 .HasOne(bc => bc.ApplicationUser)
				 .WithMany(b => b.ApplicationUserGroups)
				 .HasForeignKey(bc => bc.ApplicationUserId);
			modelBuilder.Entity<ApplicationUserGroup>()
				 .HasOne(bc => bc.Group)
				 .WithMany(c => c.ApplicationUserGroups)
				 .HasForeignKey(bc => bc.GroupId);
		}

		public override int SaveChanges()
		{
			ChangeTracker.ApplyAuditInformation();
			return base.SaveChanges();
		}
	}
}
