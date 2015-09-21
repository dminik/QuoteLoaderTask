using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using QuoteLoader.CSV;
using Quotes;

namespace QuoteLoader.Tests
{			
	[TestFixture]
	public class QuoteImporterTests
	{
		[Test]
		public void Import_RealFile_Success()
		{
			// Arrange
			var repository = new FakeQuoteRepository();
			var importer = new QuoteImporter(repository);

			// Act	
			importer.Import(@"..\..\SampleData\quotes.txt");

			// Assert
			Assert.AreEqual(1000, repository.Count, "Invalid count of records imported!");
		}
        
		[Test]
		public void Import_TwoLines_Success()
		{
			// Arrange			
			var repositoryMock = new Mock<IQuoteRepository>();

            var mockReader = new Mock<IReader>();

            var linesQueue = new Queue<string[]>();
            linesQueue.Enqueue(new[] { "2015-08-26T13:04:32", "ABCD", "228.34" });
            linesQueue.Enqueue(new[] { "2015-08-26T13:04:33", "QWER", "228.35" });
            linesQueue.Enqueue(null);
            mockReader.Setup(x => x.Read()).Returns(linesQueue.Dequeue);
            
			var importer = new QuoteImporter(repositoryMock.Object);
		    
			// Act
            importer.Import(mockReader.Object);

			// Assert            
            repositoryMock.Verify(foo => foo.AddQuote(It.Is<Quote>(s =>
                   s.DateTime == new DateTime(2015, 8, 26, 13, 4, 32)
                && s.Ticker == "ABCD"
                && s.ValueExact == (decimal)228.34)));

            repositoryMock.Verify(foo => foo.AddQuote(It.Is<Quote>(s =>
                   s.DateTime == new DateTime(2015, 8, 26, 13, 4, 33)
                && s.Ticker == "QWER"
                && s.ValueExact == (decimal)228.35)));	
		}

		[Test]
		[ExpectedException(typeof(FormatException), ExpectedMessage = "Wrong fields number in line 1. Expected 3 but was found 2. Fields: 2015-08-26T13:04:32, ABCD")]
		public void Import_WrongFieldNumber_ThrowException()
		{
			// Arrange
			var str = "2015-08-26T13:04:32	ABCD";
			var stream = str.ToStream();

			var repository = new FakeQuoteRepository();
			var importer = new QuoteImporter(repository);
            var csv = new CsvReader(stream);

			// Act
            importer.Import(csv);
		}

		[Test]
		[ExpectedException(typeof(FormatException), ExpectedMessage = "Can't parse field 'DateTime' from string '201-08-26T13:04:32' to type 'DateTime'.")]
		public void Import_WrongDateTime_ThrowException()
		{
			// Arrange
			var str = "201-08-26T13:04:32	ABCD	228.35";
			var stream = str.ToStream();

			var repository = new FakeQuoteRepository();
			var importer = new QuoteImporter(repository);
            var csv = new CsvReader(stream);

            // Act
            importer.Import(csv);
		}

		[Test]
		[ExpectedException(typeof(FormatException), ExpectedMessage = "Can't parse field 'Value' from string '228.s35' to type 'double'.")]
		public void Import_WrongDoubleValue_ThrowException()
		{
			// Arrange
			var str = "2015-08-26T13:04:32	ABCD	228.s35";
			var stream = str.ToStream();

			var repository = new FakeQuoteRepository();
			var importer = new QuoteImporter(repository);
            var csv = new CsvReader(stream);

            // Act
            importer.Import(csv);
		}  	
	}
}
