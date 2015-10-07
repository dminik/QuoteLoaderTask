using System;

using NUnit.Framework;

using QuoteLoader.Formatters;
using QuoteLoader.Formatters.Exceptions;

using Quotes;

namespace QuoteLoader.Tests.Formatters
{			
	[TestFixture]
	public class QuoteFormatterTests
	{

		[Test]
		public void ToString_ValidParams_Success()
		{
			// Arrange						
			var quote = new Quote
			{
				DateTime = new DateTime(2015, 8, 26, 13, 4, 32),
				Ticker = "ABCD",
				ValueExact = (decimal)228.34
			};

			var expectedLine = new[] { "2015-08-26T13:04:32", "ABCD", "228.34" };
			var parser = new QuoteFormatter();

			// Act	
			var actualLine = parser.ToString(quote);

			// Assert			
			Assert.AreEqual(expectedLine, actualLine);
		}

		[Test]
		public void FromString_ValidParams_Success()
		{
			// Arrange			
			var line = new[] { "2015-08-26T13:04:32", "ABCD", "228.34" };               
				

			var expectedQuote = new Quote
			{
				DateTime = new DateTime(2015, 8, 26, 13, 4, 32),
				Ticker = "ABCD",
				ValueExact = (decimal)228.34
			};
						
			var parser = new QuoteFormatter();

			// Act	
			var quote = parser.FromString(line, 1);

			// Assert			
			Assert.IsTrue(quote.Equals(expectedQuote));			
		}

		[Test]
		[ExpectedException(typeof(WrongFieldsNumberException), ExpectedMessage = "Wrong fields number in line 1. Expected 3 but was found 2. Fields: 2015-08-26T13:04:32, ABCD")]
		public void FromString_WrongFieldNumber_ThrowException()
		{			
			// Arrange			
			var line = new[] { "2015-08-26T13:04:32", "ABCD" };			
			var parser = new QuoteFormatter();

			// Act	
			var quote = parser.FromString(line, 1);
		}

		[Test]
		[ExpectedException(typeof(QuoteParsingException), ExpectedMessage = "Can't parse a string '201-08-26T13:04:32' to type 'DateTime'.")]
		public void FromString_WrongDateTime_ThrowException()
		{
			// Arrange			
			var line = new[] { "201-08-26T13:04:32", "ABCD", "228.34" };
			var parser = new QuoteFormatter();

			// Act	
			var quote = parser.FromString(line, 1);
		}

		[Test]
		[ExpectedException(typeof(QuoteParsingException), ExpectedMessage = "Can't parse a string '228.s34' to type 'Double'.")]
		public void FromString_WrongDoubleValue_ThrowException()
		{
			// Arrange			
			var line = new[] { "2015-08-26T13:04:32", "ABCD", "228.s34" };
			var parser = new QuoteFormatter();

			// Act	
			var quote = parser.FromString(line, 1);
		}		
	}
}
