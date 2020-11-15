using Assignment2_3WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment2_3WebApi.DataAccess
{
    public class Assignment4DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Adult> Adults { get; set; }
        public DbSet<Interest> Interest { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source = D:\\rider\\Assignment2-3\\Assignment2-3WebApi\\Assignment4.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ChildInterest>()
                .HasKey(ci => new {ci.ChildId, ci.InterestId});

            modelBuilder.Entity<Family>()
                .HasKey(fam => new {fam.StreetName, fam.HouseNumber});

            modelBuilder.Entity<ChildInterest>()
                .HasOne(ci => ci.Child)
                .WithMany(child => child.ChildInterests)
                .HasForeignKey(ci => ci.ChildId);

            modelBuilder.Entity<ChildInterest>()
                .HasOne(ci => ci.Interest)
                .WithMany(i => i.ChildInterests)
                .HasForeignKey(ci => ci.InterestId);
        }
    }
}