using NUnit.Framework;
using Quotes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        internal class FakeQuoteRepository : IQuoteRepository
        {
            internal int Count = 0;

            public void AddQuote(Quote quote)
            {
                quote.Id = ++Count;
            }

            public IEnumerable<Quote> GetQuotes(DateTime start, DateTime end)
            {
                throw new NotImplementedException();
            }
        }
    }
}
