using System;
using System.IO;
using System.Linq;
using System.Text;

using Moq;

using NUnit.Framework;
using QuoteLoader.CSV;
using QuoteLoader.StorageProviders;

namespace QuoteLoader.Tests.StorageProviders.CSV
{
	[TestFixture]
	public class CsvWriterTests
	{
		const string fileName = @"..\..\SampleData\write.txt";
		const string DELIMETER_AS_SPACE = " ";
		const string DELIMETER_AS_SEMICOLON = ";";

		[Test]
		public void Write_RealFile_Success()
		{
			// Arrange
			if (File.Exists(fileName))
				File.Delete(fileName);

			try
			{
				// Act
				using (var writer = new CsvWriter(fileName, DELIMETER_AS_SPACE))
				{
					string[] values1 = { "qw", "er" };
					writer.Write(values1);
					string[] values2 = { "ty", "ui" };
					writer.Write(values2);
					writer.Close();
				}

				// Assert
				var fileLines = File.ReadAllLines(fileName);
				Assert.AreEqual(2, fileLines.Count(), "Invalid file exported");
				Assert.AreEqual("qw er", fileLines[0]);
				Assert.AreEqual("ty ui", fileLines[1]);
			}
			finally
			{
				if (File.Exists(fileName)) 
					File.Delete(fileName);
			}
		}

		[Test]
		public void Write_RealFileUTF8_Success()
		{
			// Arrange
			if (File.Exists(fileName))
				File.Delete(fileName);

			try
			{
				// Act
				using (var writer = new CsvWriter(fileName, DELIMETER_AS_SPACE))
				{
					string[] values1 = { "è", "€" };
					writer.Write(values1);
					string[] values2 = { "è2", "€2" };
					writer.Write(values2);
					writer.Close();
				}

				// Assert
				var fileLines = File.ReadAllLines(fileName);
				Assert.AreEqual(2, fileLines.Count(), "Invalid file exported");
				Assert.AreEqual("è €", fileLines[0]);
				Assert.AreEqual("è2 €2", fileLines[1]);
			}
			finally
			{
				if (File.Exists(fileName))
					File.Delete(fileName);
			}
		}

		[Test]
		public void Write_TwoStringsWithSemicolonDelimeter_Success()
		{
			// Arrange			
			using (var stream = new MemoryStream())
			{
				using (var streamWriter = new StreamWriter(stream))

				// Act
				using (var writer = new CsvWriter(streamWriter, DELIMETER_AS_SEMICOLON))
				{
					string[] values1 = { "qw", "er" };
					writer.Write(values1);
					string[] values2 = { "ty", "ui" };
					writer.Write(values2);
					writer.Close();
				}

				// Assert
				var expectedLines = string.Format("qw;er{0}ty;ui{0}", Environment.NewLine);
				var actualLines = Encoding.UTF8.GetString(stream.ToArray());
				Assert.AreEqual(expectedLines, actualLines);				
			}			
		}
	}
}
