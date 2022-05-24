using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SimpleMessaging.Message.Repository.SQL
{
    using Data;
    using Extensions;
    using Interfaces;
    using Models;

    public partial class MesssageRepository : IMesssageRepository, IDisposable
    {
        private readonly MessageRepositoryDbContext _context;
        private readonly ILogger<MesssageRepository> _logger;
 
        private bool disposed = false;

        // TODO: Add filtering by creator id if authenticated user is not an administrator or allowed by some other role?
        // TODO: Consider indexing on authenticated user (principal id)?

        public MesssageRepository(MessageRepositoryDbContext context, ILogger<MesssageRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}