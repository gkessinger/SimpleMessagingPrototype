using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace SimpleMessaging.Message.Repository.SQL.Tests
{
    using Data;
    using Models;

    [TestFixture]
    public partial class MessageRepositoryTests
    {
        private MesssageRepository repository;
        private MessageRepositoryDbContext context;
        private DbContextOptions<MessageRepositoryDbContext> options;
        private ILogger<MesssageRepository> logger;

        private const int existingMessageId = 1;
        private const string existingMessageCategoryName = "Reasons";
        private const string existingMessageCategoryDescription = "Test existing message category reason.";
        private const string existingMessageCreatorFirstName = "Test";
        private const string existingMessageCreatorLastName = "Creator";
        private const string existingMessageBody = "Esisting test message body";

        private const string testMessageCategoryName = "Reasons";
        private const string testMessageCreatorFirstName = "Test";
        private const string testMessageCreatorLastName = "Creator";
        private const string testMessageBody = "Test message body";

        [OneTimeSetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MessageRepositoryDbContext>()
                .UseInMemoryDatabase(databaseName: "SimpleMessaging.Messages")
                .Options;
            context = new MessageRepositoryDbContext(options);

            var mock = new Mock<ILogger<MesssageRepository>>();
            logger = mock.Object;

            repository = new MesssageRepository(context, logger);

        }

        /// <summary>
        /// Initialze test data prior to each test.
        /// </summary>
        /// <remarks>
        /// Unit tests of CRUD operations can alter the state of test data in multiple ways
        /// depending on the order of the test runs.  Initializing a clean set of test data
        /// prior to each test allows predictable expectations (e.g. a delete test expecting
        /// an element to exist).
        /// 
        /// Generated Ids of elements can also be problematic.  Use of context and test derived
        /// unique values to obtain the generated Id for comparison is one method of dealing
        /// with this issue.
        /// 
        /// An alternative would be to use a unique identifier (UID) in addtion
        /// or in place of the generated Id.  This is model design dependent.
        /// </remarks>
        [SetUp]
        public void TestSetup()
        {
            Initialize();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            context?.Dispose();
            repository?.Dispose();
            options = null;
            context = null;
            repository = null;
        }

        private void Initialize()
        {
            context.Messages.ToList().ForEach(message => context.Messages.Remove(message));
            context.Categories.ToList().ForEach(category => context.Categories.Remove(category));
            context.Creators.ToList().ForEach(creator => context.Creators.Remove(creator));
            context.SaveChanges();

            var existingMessage = new Message
            {
                Category = new MessageCategory
                {
                    Name = existingMessageCategoryName,
                    Description = existingMessageCategoryDescription
                },
                Creator = new MessageCreator
                {
                    FirstName = existingMessageCreatorFirstName,
                    LastName = existingMessageCreatorLastName,
                },
                PostDate = System.DateTime.UtcNow,
                Body = existingMessageBody
            };

            context.Messages.Add(existingMessage);
            context.SaveChanges();
        }

        private static Message CreateTestMessage(string testName)
        {
            var message = new Message
            {
                Category = new MessageCategory
                {
                    Name = testMessageCategoryName
                },
                Creator = new MessageCreator
                {
                    FirstName = testMessageCreatorFirstName,
                    LastName = testMessageCreatorLastName,
                },
                PostDate = System.DateTime.UtcNow,
                Body = $"{testName} - {testMessageBody}"
            };

            return message;
        }

        private static MessageCategory CreateTestMessageCategory(string testName, string categoryName)
        {
            var result = new MessageCategory
            {
                Name = categoryName,
                Description = testName
            };

            return result;
        }

        private static MessageCreator CreateTestMessageCreator(string testName)
        {
            var result = new MessageCreator
            {
                FirstName = testMessageCreatorFirstName,
                LastName = testName
            };

            return result;
        }
    }
}