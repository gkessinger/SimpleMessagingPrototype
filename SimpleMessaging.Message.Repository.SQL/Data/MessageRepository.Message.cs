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
        public async Task<RepositoryStatusCode> DeleteMessagesAsync(int[] ids)
        {
            try
            {
                var messages = _context.Messages.Where(x => ids.Contains(x.Id));

                if (messages.Any())
                {
                    _context.Messages.RemoveRange(messages);
                    await SaveAsync();

                    return RepositoryStatusCode.Ok;
                }
                return RepositoryStatusCode.NotFound;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to delete messages.");
                return RepositoryStatusCode.ServerError;
            }
        }

        public async Task<(Message, RepositoryStatusCode)> CreateMessageAsync(Message message)
        {
            try
            {
                var messages = _context.Messages.Where(x => x.Id == message.Id);

                if (messages.Any())
                {
                    _logger.LogError($"Failed to create message, message already exists.");
                    return (null, RepositoryStatusCode.Conflict);
                }
                else
                {
                    var result = await _context.Messages.AddAsync(message);
                    await SaveAsync();
                    return (result.Entity, RepositoryStatusCode.Created);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to create messages.");
                return (null, RepositoryStatusCode.ServerError);
            }
        }

        public async Task<(Message, RepositoryStatusCode)> GetMessageByIdAsync(int id)
        {
            try
            {
                var result = await _context.Messages.Where(x => x.Id == id).FirstOrDefaultAsync();
                return result != null ? (result, RepositoryStatusCode.Ok) : (result, RepositoryStatusCode.NotFound);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to create messages.");
                return (null,  RepositoryStatusCode.ServerError);
            }
        }

        public async Task<(IEnumerable<Message>, RepositoryStatusCode)> GetMessagesAsync()
        {
            try
            {
                var result= await _context.Messages.ToListAsync();
                return result != null ? (result, RepositoryStatusCode.Read) : (result, RepositoryStatusCode.NotFound);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to create messages.");
                return (Enumerable.Empty<Message>(), RepositoryStatusCode.ServerError);
            }
        }

        public async Task<RepositoryStatusCode> UpdateMessageAsync(Message target, Message source)
        {
            try
            {
                var message = await _context.Messages.SingleOrDefaultAsync(x => x.Id == target.Id);

                if (message == null)
                {
                    _logger.LogError($"Failed to update message, message does not exist.");
                    return RepositoryStatusCode.NotFound;
                }
                else
                {
                    message.UpdateFrom(source);
                    await SaveAsync();
                    return RepositoryStatusCode.Updated;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to update messages.");
                return RepositoryStatusCode.ServerError;
            }
        }

        public async Task<RepositoryStatusCode> DeleteMessageAsync(int id)
        {
            try
            {
                var message = _context.Messages.SingleOrDefault(x => x.Id == id);

                if (message != null)
                {
                    _context.Messages.Remove(message);
                    await SaveAsync();
                    return RepositoryStatusCode.Deleted;
                }

                return RepositoryStatusCode.NotFound;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete message.");
                return RepositoryStatusCode.ServerError;
            }
        }
    }
}