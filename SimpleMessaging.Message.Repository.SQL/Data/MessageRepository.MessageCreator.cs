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
        public async Task CreateMessageCreatorAsync(MessageCreator source)
        {
            try
            {
                await _context.Creators.AddAsync(source);
                await SaveAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create message creator");
            }
        }

        public async Task<MessageCreator> GetMessageCreatorByIdAsync(int id)
        {
            try
            {
                return await _context.Creators.SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get message creator by id.");
                return null;
            }
        }

        public async Task<IEnumerable<MessageCreator>> GetMessageCreatorsAsync()
        {
            try
            {
                return await _context.Creators.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get message creator.");
                return Enumerable.Empty<MessageCreator>();
            }
        }

        public async Task UpdateMessageCreatorAsync(MessageCreator target, MessageCreator source)
        {
            try
            {
                if (target == null) throw new ArgumentNullException(nameof(target));
                if (source == null) throw new ArgumentNullException(nameof(source));

                target.UpdateFrom(source);

                await SaveAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update message creator.");
            }
        }

        public async Task DeleteMessageCreatorAsync(int id)
        {
            try
            {
                var result = _context.Creators.SingleOrDefault(x => x.Id == id);

                if (result != null)
                {
                    _context.Creators.Remove(result);
                    await SaveAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete message creator.");
            }
        }

        public async Task DeleteMessageCreatorsAsync(int[] ids)
        {
            try
            {
                var result = _context.Creators.Where(x => ids.Contains(x.Id)).ToList();

                if (result != null)
                {
                    _context.Creators.RemoveRange(result);
                    await SaveAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete message creators.");
            }
        }
    }
}