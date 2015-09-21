using NUnit.Framework;
using Quotes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;

namespace QuoteLoader.Tests
{
	[TestFixture]
	public class QuoteExporterTests
	{
		[Test]
		public void Export_RealFile_Success()
		{
			// Arrange
			var testQuotes = new List<Quote>()
            {
                new Quote { Id = 1, DateTime = new DateTime(2015, 8, 26, 13, 4, 32), Ticker = "ABCD", ValueExact = (decimal) 228.34 },
                new Quote { Id = 2, DateTime = new DateTime(2015, 8, 26, 13, 4, 33), Ticker = "QWER", ValueExact = (decimal) 228.35 },
            };
			
            var repositoryMock = new Mock<IQuoteRepository>();
            repositoryMock.Setup(x => x.GetQuotes(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(testQuotes);
            
            var exporter = new QuoteExporter(repositoryMock.Object);

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
