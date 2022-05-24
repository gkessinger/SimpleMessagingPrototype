namespace SimpleMessaging.Message.Repository.Interfaces
{
    using Models;

    public enum RepositoryStatusCode { Ok, Created, Read, Updated, Deleted, ServerError, NotFound, Conflict, Invalid }

    public partial interface IMesssageRepository : IDisposable
    {
        Task<(Message, RepositoryStatusCode)> CreateMessageAsync(Message message);
        Task<(Message, RepositoryStatusCode)> GetMessageByIdAsync(int id);
        Task<(IEnumerable<Message>, RepositoryStatusCode)> GetMessagesAsync();
        Task<RepositoryStatusCode> UpdateMessageAsync(Message target, Message source);
        Task<RepositoryStatusCode> DeleteMessageAsync(int id);
        Task<RepositoryStatusCode> DeleteMessagesAsync(int[] ids);

        Task CreateMessageCategoryAsync(MessageCategory message);
        Task<MessageCategory> GetMessageCategoryByIdAsync(int id);
        Task<IEnumerable<MessageCategory>> GetMessageCategoriesAsync();
        Task UpdateMessageCategoryAsync(MessageCategory target, MessageCategory source);
        Task DeleteMessageCategoryAsync(int id);
        Task DeleteMessageCategoriesAsync(int[] ids);

        Task CreateMessageCreatorAsync(MessageCreator message);
        Task<MessageCreator> GetMessageCreatorByIdAsync(int id);
        Task<IEnumerable<MessageCreator>> GetMessageCreatorsAsync();
        Task UpdateMessageCreatorAsync(MessageCreator target, MessageCreator source);
        Task DeleteMessageCreatorAsync(int id);
        Task DeleteMessageCreatorsAsync(int[] ids);

        Task SaveAsync();
    }
}