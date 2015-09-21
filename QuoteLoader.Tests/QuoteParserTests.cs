using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Quotes;

namespace QuoteLoader.Tests
{			
	[TestFixture]
	public class QuoteParserTests
	{		

		[Test]
		public void Parse_ValidParams_Success()
		{
			// Arrange			
			var line = new[] { "2015-08-26T13:04:32", "ABCD", "228.34" };               
				

			var expectedQuote = new Quote
			{
				DateTime = new DateTime(2015, 8, 26, 13, 4, 32),
				Ticker = "ABCD",
				ValueExact = (decimal)228.34
			};
						
			var parser = new QuoteParser();

			// Act	
			var quote = parser.Parse(line, 1);

			// Assert			
			Assert.IsTrue(quote.Equals(expectedQuote));			
		}

		[Test]
		[ExpectedException(typeof(FormatException), ExpectedMessage = "Wrong fields number in line 1. Expected 3 but was found 2. Fields: 2015-08-26T13:04:32, ABCD")]
		public void Parse_WrongFieldNumber_ThrowException()
		{			
			// Arrange			
			var line = new[] { "2015-08-26T13:04:32", "ABCD" };			
			var parser = new QuoteParser();

			// Act	
			var quote = parser.Parse(line, 1);
		}

		[Test]
		[ExpectedException(typeof(FormatException), ExpectedMessage = "Can't parse field 'DateTime' from string '201-08-26T13:04:32' to type 'DateTime'.")]
		public void Parse_WrongDateTime_ThrowException()
		{
			// Arrange			
			var line = new[] { "201-08-26T13:04:32", "ABCD", "228.34" };
			var parser = new QuoteParser();

			// Act	
			var quote = parser.Parse(line, 1);
		}

		[Test]
		[ExpectedException(typeof(FormatException), ExpectedMessage = "Can't parse field 'Value' from string '228.s34' to type 'double'.")]
		public void Parse_WrongDoubleValue_ThrowException()
		{
			// Arrange			
			var line = new[] { "2015-08-26T13:04:32", "ABCD", "228.s34" };
			var parser = new QuoteParser();

			// Act	
			var quote = parser.Parse(line, 1);
		}
		
	}
}
