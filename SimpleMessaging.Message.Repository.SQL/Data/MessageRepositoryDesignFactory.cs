using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SimpleMessaging.Message.Repository.SQL.Data
{
    public class MessageRepositoryDesignFactory : IDesignTimeDbContextFactory<MessageRepositoryDbContext>
    {
        public MessageRepositoryDesignFactory()
        {
        }

        private static IConfiguration Configuration => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public MessageRepositoryDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MessageRepositoryDbContext>();
            builder.UseSqlServer(Configuration.GetConnectionString("MessageRepositoryDbContextConnection"));
            return new MessageRepositoryDbContext(builder.Options);
        }
    }
}