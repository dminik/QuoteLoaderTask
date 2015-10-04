using System.IO;
using System.Linq;

using NUnit.Framework;

using QuoteLoader.CSV;
using QuoteLoader.StorageProviders;

namespace QuoteLoader.Tests.StorageProviders.CSV
{
	[TestFixture]
	public class CsvWriterTests
	{		
		[Test]
		public void Write_RealFile_Success()
		{
			const string fileName = @"..\..\SampleData\write.txt";
			if (File.Exists(fileName))
				File.Delete(fileName);

			try
			{
				// Act
				using (var reader = new CsvWriter(fileName, " "))
				{
					string[] values1 = { "qw", "er" };
					reader.Write(values1);
					string[] values2 = { "ty", "ui" };
					reader.Write(values2);
					reader.Close();
				}

				// Assert
				var fileLines = File.ReadAllLines(fileName);
				Assert.AreEqual(2, fileLines.Count(), "Invalid file exported");
				Assert.AreEqual("qw er", fileLines[0]);
				Assert.AreEqual("ty ui", fileLines[1]);
			}
			finally
			{
				if (File.Exists(fileName)) File.Delete(fileName);
			}
		}

		[Test]
		[ExpectedException(typeof(FileExistsException))]
		public void Write_RealFileExists_ThrowException()
		{
			const string fileName = @"..\..\SampleData\writeToCheckExistTest.txt";

			// Arrange 
			try
			{
				if (!File.Exists(fileName))
					File.WriteAllText(fileName, "testLine");

				// Act
				using(new CsvWriter(fileName))
				{					
				}
			}			
			finally
			{
				if (File.Exists(fileName))
					File.Delete(fileName);
			}
		}
	}
}
