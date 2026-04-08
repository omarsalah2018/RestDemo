using Microsoft.EntityFrameworkCore;
using RestDemo.Data.Models;
using System.Reflection.Metadata;

namespace RestDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //global query filter for rooms with capacity greater than 0
            //modelBuilder.Entity<Room>().HasQueryFilter(x => x.Capastiy > 0);

            modelBuilder.Entity<User>()
                .HasOne(e => e.Role)
                .WithOne(e => e.User)
                .HasForeignKey<User>(e => e.RoleId)
                .IsRequired();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<UserRooms> UserRooms { get; set; }
    }
}