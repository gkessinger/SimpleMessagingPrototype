using Microsoft.EntityFrameworkCore;

namespace SimpleMessaging.Message.Repository.SQL.Data
{
    using Models;

    public class MessageRepositoryDbContext : DbContext
    {
        public MessageRepositoryDbContext(DbContextOptions<MessageRepositoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageCategory> Categories { get; set; }
        public DbSet<MessageCreator> Creators { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().HasKey(e => e.Id);

            modelBuilder.Entity<MessageCategory>().HasKey(e => e.Id);
            modelBuilder.Entity<MessageCategory>().Property(e => e.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<MessageCategory>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<MessageCreator>().HasKey(e => e.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}