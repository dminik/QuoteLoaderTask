using System;
using System.IO;

namespace QuoteLoader.CSV
{	
	public class CsvReader : ICsvReader, IDisposable
	{
		private StreamReader _reader;
		private bool _disposed;
		private readonly char _delimiter;

		public CsvReader(string fileName, char delimiter = '\t')
		{
			_delimiter = delimiter;

			if (!File.Exists(fileName))			
				throw new FileNotFoundException(fileName);
						
			_reader = new StreamReader(fileName);
		}

		internal CsvReader(StreamReader stream, char delimiter = '\t')
		{
			_delimiter = delimiter;
			_reader = stream;
		}

		public bool Read(out string[] values)
		{
			values = new string[0];

			if (_reader.Peek() > -1)
			{
				var line = _reader.ReadLine();

				StringSplitOptions opt = StringSplitOptions.RemoveEmptyEntries;
				if (line != null)
				{
					values = line.Split(new string[] { _delimiter.ToString() }, opt);
				}
				
				return true;
			}
			
			return false;			
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
					if (_reader != null)
					{
						_reader.Close();
						_reader = null;
					}
				}

				_disposed = true;
			}
		}
		
		~CsvReader()
		{			
			Dispose(false);
		}
	}
}
