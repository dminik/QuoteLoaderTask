﻿using System;
using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using QuoteLoader.Formatters;
using QuoteLoader.StorageProviders;

using Quotes;

namespace QuoteLoader.Tests.ExportersImportersFasade
{			
	[TestFixture]
	public class QuoteImporterBaseTests
	{
		Mock<IQuoteRepository> _repositoryMock;

		[SetUp]
		public void Init()
		{	        
			_repositoryMock = new Mock<IQuoteRepository>();
		}
		
		[Test]
		public void Import_TwoLines_Success()
		{
			// Arrange						
			var testLinesFields = new List<string[]>()
			{
				new[] { "xxx" },                
				new[] { "yyy" },                
			};
			
			var mockReader = CreateMockReader(testLinesFields);
			var importer = new QuoteImporterBase(_repositoryMock.Object);

			var formatter = new Mock<IQuoteFormatter>();
			formatter.Setup(foo => foo.FromString(It.IsAny<string[]>(), It.IsAny<uint>())).Returns(new Quote());
			
			// Act
			importer.Import(mockReader.Object, formatter.Object);

			// Assert            
			_repositoryMock.Verify(foo => foo.AddQuote(It.IsAny<Quote>()), Times.Exactly(2));	
		}

		[Test]
		public void Import_TwoLinesAndEmptyLines_SkipEmptyLine()
		{
			// Arrange						
			var testLinesFields = new List<string[]>()
			{
				new[] { "xxx" }, 
				new[] { string.Empty },
				new[] { " " },
				new[] { " \t " },
				new[] { "  " },
				new[] { "yyy" },
			};

			var mockReader = CreateMockReader(testLinesFields);
			var importer = new QuoteImporterBase(_repositoryMock.Object);

			var formatter = new Mock<IQuoteFormatter>();
			formatter.Setup(foo => foo.FromString(It.IsAny<string[]>(), It.IsAny<uint>())).Returns(new Quote());

			// Act
			importer.Import(mockReader.Object, formatter.Object);

			// Assert            
			_repositoryMock.Verify(foo => foo.AddQuote(It.IsAny<Quote>()), Times.Exactly(2));
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Import_ReaderParamIsNull_ThrowException()
		{
			// Arrange						
			var importer = new QuoteImporterBase(_repositoryMock.Object);
			var formatter = new Mock<IQuoteFormatter>();
			
			// Act
			importer.Import(null, formatter.Object);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Import_FormatterParamIsNull_ThrowException()
		{
			// Arrange						
			var mockReader = CreateMockReader(new List<string[]>());
			var importer = new QuoteImporterBase(_repositoryMock.Object);
			
			// Act
			importer.Import(mockReader.Object, null);
		}
		
		public static Mock<IReader> CreateMockReader(List<string[]> testLinesFields)
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
