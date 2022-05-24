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
        public async Task CreateMessageCategoryAsync(MessageCategory source)
        {
            try
            {
                await _context.Categories.AddAsync(source);
                await SaveAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create message category");
            }
        }

        public async Task<MessageCategory> GetMessageCategoryByIdAsync(int id)
        {
            try
            {
                return await _context.Categories.SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get message category by id.");
                return null;
            }
        }

        public async Task<IEnumerable<MessageCategory>> GetMessageCategoriesAsync()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get message categories.");
                return Enumerable.Empty<MessageCategory>();
            }
        }

        public async Task UpdateMessageCategoryAsync(MessageCategory target, MessageCategory source)
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
                _logger.LogError(e, "Failed to update message category.");
            }
        }

        public async Task DeleteMessageCategoryAsync(int id)
        {
            try
            {
                var result = _context.Categories.SingleOrDefault(x => x.Id == id);

                if (result != null)
                {
                    _context.Categories.Remove(result);
                    await SaveAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete message category.");
            }
        }

        public async Task DeleteMessageCategoriesAsync(int[] ids)
        {
            try
            {
                var result = _context.Categories.Where(x => ids.Contains(x.Id)).ToList();

                if (result != null)
                {
                    _context.Categories.RemoveRange(result);
                    await SaveAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete message categories.");
            }
        }
    }
}