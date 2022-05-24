using FluentAssertions;
using NUnit.Framework;
    using System.Collections.Generic;
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
        public async Task GetMessages_ShouldReturnAllMessages()
        {
            var message = CreateTestMessage(nameof(GetMessages_ShouldReturnAllMessages));
            await context.Messages.AddAsync(message);
            await context.SaveChangesAsync();

            var (messages, status) = await repository.GetMessagesAsync();

            messages.Should().NotBeNullOrEmpty();
            messages.Count().Should().BeGreaterThan(0);
            messages.Should().BeOfType(typeof(List<Message>));
        }

        [Test]
        public async Task GetMessageById_ShouldReturnMessageById()
        {
            var message = CreateTestMessage(nameof(GetMessages_ShouldReturnAllMessages));

            var id = context.Messages.Single(x => x.Body == existingMessageBody).Id;

            await context.Messages.AddAsync(message);
            await context.SaveChangesAsync();

            var (result, status) = await repository.GetMessageByIdAsync(id);

            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Test]
        public async Task GetMessageCategories_ShouldReturnAllMessageCategories()
        {
            var response = await repository.GetMessageCategoriesAsync();

            response.Should().NotBeNullOrEmpty();
            response.Count().Should().BeGreaterThan(0);
            response.Should().BeOfType(typeof(List<MessageCategory>));
        }

        [Test]
        public async Task GetMessageCategoryById_ShouldReturnMessageCategoryById()
        {
            var ids = context.Categories.Select(x => x.Id).ToList();
            var response = await repository.GetMessageCategoryByIdAsync(ids[0]);

            response.Should().NotBeNull();
            response.Id.Should().Be(ids[0]);
            response.Name.Should().Be(existingMessageCategoryName);
            response.Description.Should().Be(existingMessageCategoryDescription);
        }

        [Test]
        public async Task GetMessageCreators_ShouldReturnAllMessageCreators()
        {
            var response = await repository.GetMessageCreatorsAsync();

            response.Should().NotBeNullOrEmpty();
            response.Count().Should().BeGreaterThan(0);
            response.Should().BeOfType(typeof(List<MessageCreator>));
        }

        [Test]
        public async Task GetMessageCreatorById_ShouldReturnMessageCreatorById()
        {
            var ids = context.Creators.Select(x => x.Id).ToList();
            var response = await repository.GetMessageCreatorByIdAsync(ids[0]);

            response.Should().NotBeNull();
            response.Id.Should().Be(ids[0]);
            response.FirstName.Should().Be(existingMessageCreatorFirstName);
            response.LastName.Should().Be(existingMessageCreatorLastName);
        }
    }
}