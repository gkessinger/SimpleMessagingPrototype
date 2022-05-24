using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMessaging.Message.Repository.SQL.Tests
{
    using Models;
    using Interfaces;

    [TestFixture]
    public partial class MessageRepositoryTests
    {
        [Test]
        public async Task CreateMessage_ShouldCreateTestMessage()
        {
            var source = CreateTestMessage(nameof(CreateMessage_ShouldCreateTestMessage));

            var (result, status) = await repository.CreateMessageAsync(source);

            var message = context.Messages.Where(x => x.Body == source.Body).SingleOrDefault();

            message.Should().NotBeNull();
            message.Should().BeOfType(typeof(Message));
            message.Id.Should().BeGreaterThan(0);
            message.Body.Should().Be(source.Body);
            message.Category.Should().Be(source.Category);
        }

        [Test]
        public async Task CreateMessageCategory_ShouldCreateTestMessageCategory()
        {
            var source = CreateTestMessageCategory(nameof(CreateMessageCategory_ShouldCreateTestMessageCategory), "Reason");

            await repository.CreateMessageCategoryAsync(source);

            var result = context.Categories.Where(x => x.Name == source.Name).SingleOrDefault();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(MessageCategory));
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().Be(source.Name);
            result.Description.Should().Be(source.Description);
        }

        [Test]
        public async Task CreateMessageCreator_ShouldCreateTestMessageCreator()
        {
            var source = CreateTestMessageCreator(nameof(CreateMessageCreator_ShouldCreateTestMessageCreator));

            await repository.CreateMessageCreatorAsync(source);

            var result = context.Creators.Single(x => x.LastName == source.LastName);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(MessageCreator));
            result.Id.Should().BeGreaterThan(0);
            result.FirstName.Should().Be(source.FirstName);
            result.LastName.Should().Be(source.LastName);
        }
    }
}