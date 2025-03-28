using Microsoft.EntityFrameworkCore;

/*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configBuilder = new ConfigurationBuilder();

            configBuilder.SetBasePath(Directory.GetCurrentDirectory());

            configBuilder.AddJsonFile("appsettings.json");

            var config = configBuilder.Build();

            string connectionString = config.GetConnectionString("DefaultConnection")!;

            optionsBuilder.UseNpgsql(connectionString);
        }*/

namespace Contacts.Contexts
{
    
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<ContactBook> ContactBooks { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.ContactBooks)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<ContactBook>()
                .HasMany(c => c.Contacts)
                .WithOne(c => c.ContactBook)
                .HasForeignKey(c => c.ContactBookId);

            
        }
    }
}
