using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;


namespace SimpleMessaging.Message.Repository.SQL.Tests
{
    using Interfaces;

    [TestFixture]
    public partial class MessageRepositoryTests
    {
        [Test]
        public async Task DeleteMessages_ShouldDeleteAllMessagesByIds()
        {
            CreateTestMessage(nameof(DeleteMessages_ShouldDeleteAllMessagesByIds));

            var ids = context.Messages.Select(x => x.Id).ToList();
            var status = await repository.DeleteMessagesAsync(ids.ToArray());
            var messages = await context.Messages.ToListAsync();

            messages.Count.Should().BeLessThan(1);
        }

        [Test]
        public async Task DeleteMessages_ShouldDeleteMessageById()
        {
            var status = await repository.DeleteMessageAsync(1);
            var message = context.Messages.FirstOrDefault(x => x.Id == 1);

            message.Should().BeNull();
        }

        // TODO: the DeleteMessageCategoryById should fail if a Category is in use by a message, need to configure FK relationship to Messages and restrict..
        [Test]
        public async Task DeleteMessageCategoryById_ShouldDeleteMessageCategoryById()
        {
            var id = context.Messages.First().Category.Id;
            await repository.DeleteMessageCategoryAsync(id);

            var result = context.Categories.FirstOrDefault(x => x.Id == id);

            result.Should().BeNull();
        }

        // TODO: the DeleteMessageCreatorById should fail if a Creator is in use by a message, need to configure FK relationship to Messages and restrict..
        [Test]
        public async Task DeleteMessageCreatorById_ShouldDeleteMessageCreatorById()
        {
            var id = context.Messages.First().Category.Id;
            await repository.DeleteMessageCreatorAsync(id);

            var result = context.Creators.FirstOrDefault(x => x.Id == id);

            result.Should().BeNull();
        }
    }
}