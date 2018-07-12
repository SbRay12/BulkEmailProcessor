using BulkEmail.Processor;
using NUnit.Framework;

namespace BulkEmail.Tests
{
    [TestFixture]
    public class BulkEmailProcessorTests
    {
        private FakeEmailProvider fakeEmailProvider;
        private const string TestInputFile = @"test_data\contacts.csv";

        [SetUp]
        public void SetUp()
        {
            fakeEmailProvider = new FakeEmailProvider();
        }

        [Test]
        public void Should_send_mail_using_mailshot_service()
        {
            var processor = new BulkEmailProcessor(fakeEmailProvider);

            processor.Process(TestInputFile);

            Assert.That(fakeEmailProvider.Counter, Is.EqualTo(229));
        }

        private class FakeEmailProvider : IEmailProvider
        {
            internal int Counter { get; private set; }

            public void Send(string name, string address)
            {
                Counter++;
            }
        }
    }
}