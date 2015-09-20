using NUnit.Framework;

namespace QuoteLoader.Tests
{
	using System;
	using System.IO;
	using System.Linq;

	using QuoteLoader.CSV;

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
			var str = "2015-08-26T13:04:32	ABCD	228.34 \n\n 2015-08-26T13:04:33	QWER	228.35";
			var stream = str.ToStream();

			var repository = new FakeQuoteRepository();
			var importer = new QuoteImporter(repository);

			// Act
			importer.Import(stream);

			// Assert
			Assert.AreEqual(2, repository.Count, "Invalid count of records imported!");

			Assert.AreEqual(new DateTime(2015, 8, 26, 13, 4, 32), repository.Data[0].DateTime);
			Assert.AreEqual("ABCD", repository.Data[0].Ticker);
			Assert.AreEqual((double) 228.34, repository.Data[0].Value);

			Assert.AreEqual(new DateTime(2015, 8, 26, 13, 4, 33), repository.Data[1].DateTime);
			Assert.AreEqual("QWER", repository.Data[1].Ticker);
			Assert.AreEqual((double) 228.35, repository.Data[1].Value);				
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

			// Act
			importer.Import(stream);
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

			// Act
			importer.Import(stream);
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

			// Act
			importer.Import(stream);
		}  	
	}
}
