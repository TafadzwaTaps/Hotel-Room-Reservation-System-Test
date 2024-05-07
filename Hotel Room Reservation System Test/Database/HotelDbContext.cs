using Microsoft.EntityFrameworkCore;
using Hotel_Room_Reservation_System_Test.Models;

namespace Hotel_Room_Reservation_System_Test.Databases
{
    public class HotelDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public HotelDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Room> Room { get; set; }
        public DbSet<RoomImages> RoomImages { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<InvoiceGeneration> InvoiceGeneration { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Facility> Facility { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<CancellationPolicy> CancellationPolicy { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("HotelRoomDatabaseConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasKey(b => b.Id);
            modelBuilder.Entity<RoomImages>().HasKey(c => c.Id);
            modelBuilder.Entity<Hotel>().HasKey(h => h.Id);
            modelBuilder.Entity<Reservation>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<UserRole>().HasKey(m => m.UserId);
            modelBuilder.Entity<InvoiceGeneration>().HasKey(o => o.Id);
            modelBuilder.Entity<Payment>().HasKey(or => or.Id);
            modelBuilder.Entity<Review>().HasKey(s => s.Id);
            modelBuilder.Entity<Facility>().HasKey(r => r.Id);
            modelBuilder.Entity<CancellationPolicy>().HasKey(v => v.id);
        }
    }
}