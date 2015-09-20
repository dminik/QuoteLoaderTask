using NUnit.Framework;
using Quotes;
using System;
using System.IO;
using System.Linq;

namespace QuoteLoader.Tests
{
	[TestFixture]
	public class QuoteExporterTests
	{
		[Test]
		public void Export_RealFile_Success()
		{
			// Arrange
			var repository = new FakeQuoteRepository();
			repository.AddQuote(new Quote { Id = 1, DateTime = new DateTime(2015, 8, 26, 13, 4, 32), Ticker = "ABCD", Value = 228.34 });
			repository.AddQuote(new Quote { Id = 2, DateTime = new DateTime(2015, 8, 26, 13, 4, 33), Ticker = "QWER", Value = 228.35 });

			var exporter = new QuoteExporter(repository);

			// Act
			exporter.Export(@"..\..\SampleData\export.txt", DateTime.Now.AddDays(-1), DateTime.Now);

			// Assert
			var fileLines = File.ReadAllLines(@"..\..\SampleData\export.txt");
			Assert.AreEqual(2, fileLines.Count(), "Invalid file exported");

			Assert.AreEqual("2015-08-26T13:04:32	ABCD	228.34", fileLines[0]);
			Assert.AreEqual("2015-08-26T13:04:33	QWER	228.35", fileLines[1]);
		}        
	}
}
