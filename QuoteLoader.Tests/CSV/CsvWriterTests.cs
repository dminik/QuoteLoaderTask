using NUnit.Framework;
using System.Linq;
using System.IO;
using QuoteLoader.CSV;

namespace QuoteLoader.Tests.CSV
{
	[TestFixture]
	public class CsvWriterTests
	{		
		[Test]
		public void Write_RealFile_Success()
		{
			// Act
			using (var reader = new CsvWriter(@"..\..\SampleData\write.txt", " "))
			{
				string[] values1 = {"qw", "er"};
				reader.Write(values1);
				string[] values2 = { "ty", "ui" };
				reader.Write(values2);
				reader.Close();
			}

			// Assert
			var fileLines = File.ReadAllLines(@"..\..\SampleData\write.txt");
			Assert.AreEqual(2, fileLines.Count(), "Invalid file exported");
			Assert.AreEqual("qw er", fileLines[0]);
			Assert.AreEqual("ty ui", fileLines[1]);
		}		
	}
}
