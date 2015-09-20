using NUnit.Framework;

namespace QuoteLoader.Tests
{
    [TestFixture]
    public class QuoteImporterTests
    {
        [Test]
        public void Import()
        {
            var repository = new FakeQuoteRepository();
            var importer = new QuoteImporter(repository);

            importer.Import(@"..\..\SampleData\quotes.txt");
            Assert.AreEqual(1000, repository.Count, "Invalid count of records imported!");
        }        
    }
}
