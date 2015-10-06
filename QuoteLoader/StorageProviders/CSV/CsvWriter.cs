using System;
using System.IO;
using System.Text;

using QuoteLoader.StorageProviders;

namespace QuoteLoader.CSV
{
	public class CsvWriter : IWriter
	{		
		private StreamWriter _writer;

		private bool _disposed;
		private readonly string _delimiter;

		public CsvWriter(string fileName, string delimiter = "\t", bool append = true, Encoding encoding = null)
		{
			_delimiter = delimiter;

			if (encoding == null)
				encoding = Encoding.UTF8;

			_writer = new StreamWriter(fileName, append, encoding);
		}

		public CsvWriter(StreamWriter stream, string delimiter = "\t")
		{
			_delimiter = delimiter;
			_writer = stream;
		}

		public void Write(string[] data)
		{
			if (_disposed)
				throw new ObjectDisposedException(typeof(CsvReader).FullName);

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
	}
}
