using NUnit.Framework;
using System.Linq;

namespace QuoteLoader.Tests.CSV
{
	using System.IO;

	using QuoteLoader.CSV;

	[TestFixture]
	public class CsvReaderTests
	{
		[Test]
		[ExpectedException(typeof(FileNotFoundException), ExpectedMessage = "nonon.txt")]
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
				string[] values;
				var isRead = reader.Read(out values);
				Assert.IsTrue(isRead);
				Assert.IsTrue(values.Any());
			}
		}

		[Test]		
		public void Read_TwoStringsAndTwoFields_Success()
		{
			// Arrange
			var str = "123 4567 \n abc grs  ";
			var stream = GenerateStreamFromString(str);

			// Act			
			using (var reader = new CsvReader(stream, ' '))
			{
				// Assert
				string[] values;
				var isRead = reader.Read(out values);
				Assert.IsTrue(isRead);
				Assert.AreEqual(2, values.Count());
				Assert.AreEqual("123", values[0]);
				Assert.AreEqual("4567", values[1]);

				isRead = reader.Read(out values);
				Assert.IsTrue(isRead);
				Assert.AreEqual(2, values.Count());
				Assert.AreEqual("abc", values[0]);
				Assert.AreEqual("grs", values[1]);

				isRead = reader.Read(out values);
				Assert.IsFalse(isRead);

				reader.Close();
			}
		}

		StreamReader GenerateStreamFromString(string s)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return new StreamReader(stream);
		}
	}
}
