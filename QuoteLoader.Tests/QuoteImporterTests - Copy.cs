using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Quotes;

namespace QuoteLoader.Tests
{			
	[TestFixture]
	public class QuoteImporterTests2
	{
        Mock<IQuoteRepository> _repositoryMock;

	    [SetUp]
	    public void Init()
	    {	        
            _repositoryMock = new Mock<IQuoteRepository>();
	    }

		[Test]
		public void Import_RealFile_Success()
		{
			// Arrange			
            var importer = new QuoteImporter(_repositoryMock.Object);

			// Act	
			importer.Import(@"..\..\SampleData\quotes.txt");

			// Assert			
            _repositoryMock.Verify(foo => foo.AddQuote(It.IsAny<Quote>()), Times.Exactly(1000));
		}
        
		[Test]
		public void Import_TwoLines_Success()
		{
			// Arrange						
            var testLinesFields = new List<string[]>()
            {
                new[] { "2015-08-26T13:04:32", "ABCD", "228.34" },                
                new[] { "2015-08-26T13:04:33", "QWER", "228.35" },                
            };

            var mockReader = CreateMockReader(testLinesFields);
		    var importer = new QuoteImporter(_repositoryMock.Object);

            var parser = new Mock<IQuoteParser>();
		    
			// Act
            importer.Import(mockReader.Object, parser.Object);

			// Assert            
            _repositoryMock.Verify(foo => foo.AddQuote(It.Is<Quote>(s =>
                   s.DateTime == new DateTime(2015, 8, 26, 13, 4, 32)
                && s.Ticker == "ABCD"
                && s.ValueExact == (decimal)228.34)));

            _repositoryMock.Verify(foo => foo.AddQuote(It.Is<Quote>(s =>
                   s.DateTime == new DateTime(2015, 8, 26, 13, 4, 33)
                && s.Ticker == "QWER"
                && s.ValueExact == (decimal)228.35)));	
		}
	    
	    [Test]
		[ExpectedException(typeof(FormatException), ExpectedMessage = "Wrong fields number in line 1. Expected 3 but was found 2. Fields: 2015-08-26T13:04:32, ABCD")]
		public void Import_WrongFieldNumber_ThrowException()
		{
			// Arrange			            
            var testLinesFields = new List<string[]>()
            {
                new[] { "2015-08-26T13:04:32", "ABCD" },                                
            };

            var mockReader = CreateMockReader(testLinesFields);
            var importer = new QuoteImporter(_repositoryMock.Object);

			// Act
            importer.Import(mockReader.Object);
		}

		[Test]
		[ExpectedException(typeof(FormatException), ExpectedMessage = "Can't parse field 'DateTime' from string '201-08-26T13:04:32' to type 'DateTime'.")]
		public void Import_WrongDateTime_ThrowException()
		{			
            // Arrange			            
            var testLinesFields = new List<string[]>()
            {
                new[] { "201-08-26T13:04:32", "ABCD", "228.35" },                                
            };

            var mockReader = CreateMockReader(testLinesFields);
            var importer = new QuoteImporter(_repositoryMock.Object);

            // Act
            importer.Import(mockReader.Object);
		}

		[Test]
		[ExpectedException(typeof(FormatException), ExpectedMessage = "Can't parse field 'Value' from string '228.s35' to type 'double'.")]
		public void Import_WrongDoubleValue_ThrowException()
		{
            // Arrange			            
            var testLinesFields = new List<string[]>()
            {
                new[] { "2015-08-26T13:04:32", "ABCD", "228.s35" },                                
            };

            var mockReader = CreateMockReader(testLinesFields);
            var importer = new QuoteImporter(_repositoryMock.Object);

            // Act
            importer.Import(mockReader.Object);
		}

        private static Mock<IReader> CreateMockReader(List<string[]> testLinesFields)
        {
            var mockReader = new Mock<IReader>();
            var linesQueue = new Queue<string[]>();
            foreach (var currentItem in testLinesFields)
                linesQueue.Enqueue(currentItem);

            linesQueue.Enqueue(null);

            mockReader.Setup(x => x.Read()).Returns(linesQueue.Dequeue);
            return mockReader;
        }
	}
}
