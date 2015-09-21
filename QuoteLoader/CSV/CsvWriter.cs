using System;
using System.IO;

namespace QuoteLoader.CSV
{
	public class CsvWriter : IWriter
	{		
		private StreamWriter _writer;

		private bool _disposed;
		private readonly string _delimiter;

		public CsvWriter(string fileName, string delimiter = "\t")
		{
			_delimiter = delimiter;

			if (File.Exists(fileName))			
				File.Delete(fileName);
						
			_writer = new StreamWriter(fileName);
		}

		public void Write(string[] data)
		{
			var line = string.Join(_delimiter, data);
			_writer.WriteLine(line);
		}

		public void Close()
		{
			Dispose();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					if (_writer != null)
					{
						_writer.Close();
						_writer = null;
					}
				}

				_disposed = true;
			}
		}

		~CsvWriter()
		{			
			Dispose(false);
		}
	}
}
