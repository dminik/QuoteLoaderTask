﻿using System.IO;
using System.Linq;

using NUnit.Framework;

using QuoteLoader.CSV;
using QuoteLoader.Tests.Helpers;

namespace QuoteLoader.Tests.StorageProviders.CSV
{
	[TestFixture]
	public class CsvReaderTests
	{
		[Test]
		[ExpectedException(typeof(FileNotFoundException))]
		public void Ctor_NoExistsFilePath_throwFileNotFoundException()
		{
			using (var reader = new CsvReader(@"nonon.txt"))
			{
			}			
		}

		[Test]
		public void Read_RealFile_Success()
		{
			using (var reader = new CsvReader(@"..\..\SampleData\quotes.txt"))
			{
				var values = reader.Read();				
				Assert.IsNotNull(values);
				Assert.IsTrue(values.Any());
			}
		}

		[Test]		
		public void Read_TwoStringsAndTwoFields_Success()
		{
			// Arrange
			var str = "123 4567\nabc grs";

			using (var stream = str.ToStream())
			{
				// Act			
				using (var reader = new CsvReader(stream, ' '))
				{
					// Assert
					string[] values = null;
					values = reader.Read();
					Assert.IsNotNull(values);
					Assert.AreEqual(2, values.Count());
					Assert.AreEqual("123", values[0]);
					Assert.AreEqual("4567", values[1]);

					values = reader.Read();
					Assert.IsNotNull(values);
					Assert.AreEqual(2, values.Count());
					Assert.AreEqual("abc", values[0]);
					Assert.AreEqual("grs", values[1]);

					values = reader.Read();
					Assert.IsNull(values);

					reader.Close();
				}
			}
		}

		[Test]
		public void Read_StringWithTwoFilledFieldsAndOneEmptyField_Success()
		{
			// Arrange
			const char DELIMETER_AS_SPACE = ' ';
			const string INPUT_STR = "123  45";

			using (var inputStream = INPUT_STR.ToStream())
			{
				// Act			
				using (var inputReader = new CsvReader(inputStream, DELIMETER_AS_SPACE))
				{
					// Assert
					string[] values = null;
					values = inputReader.Read();
					Assert.IsNotNull(values);
					Assert.AreEqual(3, values.Count());
					Assert.AreEqual("123", values[0]);
					Assert.AreEqual("", values[1]);
					Assert.AreEqual("45", values[2]);									
				}
			}
		}		


	}
}
