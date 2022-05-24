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
        public async Task UpdateMessage_ShouldUpdateTestMessage()
        {
            var target = context.Messages.First();
            var source = CreateTestMessage(nameof(UpdateMessage_ShouldUpdateTestMessage));

            var status = await repository.UpdateMessageAsync(target, source);

            var message = context.Messages.Where(x => x.Body == source.Body).SingleOrDefault();

            message.Should().NotBeNull();
            message.Should().BeOfType(typeof(Message));
            message.Id.Should().Be(target.Id);
            message.Body.Should().Be(source.Body);
            message.Category.Should().Be(source.Category);
            message.Creator.Should().Be(source.Creator);
        }

        [Test]
        public async Task UpdateMessageCategory_ShouldUpdateTestMessageCategory()
        {
            var target = context.Categories.First();
            var source = CreateTestMessageCategory(nameof(UpdateMessageCategory_ShouldUpdateTestMessageCategory), "ModifiedCategoryName");

            await repository.UpdateMessageCategoryAsync(target, source);

            var result = context.Categories.Single(x => x.Id == target.Id);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(MessageCategory));
            result.Id.Should().Be(target.Id);
            result.Name.Should().Be(source.Name);
            result.Description.Should().Be(source.Description);
        }

        [Test]
        public async Task UpdateMessageCreator_ShouldUpdateTestMessageCreator()
        {
            var target = context.Creators.First();
            var source = CreateTestMessageCreator(nameof(UpdateMessageCreator_ShouldUpdateTestMessageCreator));

            await repository.UpdateMessageCreatorAsync(target, source);

            var result = context.Creators.Single(x => x.Id == target.Id);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(MessageCreator));
            result.Id.Should().Be(target.Id);
            result.FirstName.Should().Be(source.FirstName);
            result.LastName.Should().Be(source.LastName);
        }
    }
}