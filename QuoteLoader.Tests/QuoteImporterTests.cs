using Moq;
using NUnit.Framework;
using Quotes;

namespace QuoteLoader.Tests
{			
	[TestFixture]
	public class QuoteImporterTests
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
	}
}
