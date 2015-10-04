using System;
using System.IO;

using QuoteLoader.StorageProviders;

namespace QuoteLoader.CSV
{	
	public class CsvReader : IReader
	{
		private StreamReader _reader;
		private bool _disposed;
		private readonly char _delimiter;

		public CsvReader(string fileName, char delimiter = '\t')
		{
			_delimiter = delimiter;
			_reader = new StreamReader(fileName);
		}

		internal CsvReader(StreamReader stream, char delimiter = '\t')
		{
			_delimiter = delimiter;
			_reader = stream;
		}

		public string[] Read()
		{
			var values = new string[0];

			if (_reader.Peek() > -1)
			{
				var line = _reader.ReadLine();
				
				if (line != null)				
					values = line.Split(new string[] { _delimiter.ToString() }, StringSplitOptions.RemoveEmptyEntries);				

				return values;      
			}
			
			return null;			
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
	}
}
