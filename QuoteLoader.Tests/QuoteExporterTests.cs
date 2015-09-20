using NUnit.Framework;
using Quotes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuoteLoader.Tests
{
    [TestFixture]
    public class QuoteExporterTests
    {
        [Test]
        public void Export()
        {
            var repository = new FakeQuoteRepository();
            var exporter = new QuoteExporter(repository);

            exporter.Export(@"..\..\SampleData\export.txt", DateTime.Now.AddDays(-1), DateTime.Now);

            var file = File.ReadAllLines(@"..\..\SampleData\export.txt");
            Assert.AreEqual(2, file.Length, "Invalid file exported");
        }

        internal class FakeQuoteRepository : IQuoteRepository
        {
            public void AddQuote(Quote quote)
            {
            }

            public IEnumerable<Quote> GetQuotes(DateTime start, DateTime end)
            {
                return new Quote[]
                {
                    new Quote { Id = 1, DateTime = DateTime.Now, Ticker = "ABCD", Value = 50 },
                    new Quote { Id = 2, DateTime = DateTime.Now, Ticker = "QWER", Value = 80 },
                };
            }
        }
    }
}
