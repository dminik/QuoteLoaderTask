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
			    var values = reader.Read();				
                Assert.IsNotNull(values);
				Assert.IsTrue(values.Any());
			}
		}

		[Test]		
		public void Read_TwoStringsAndTwoFields_Success()
		{
			// Arrange
			var str = "123 4567 \n abc grs  ";

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
	}
}
