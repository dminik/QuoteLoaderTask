using System;

namespace QuoteLoader.StorageProviders
{
	public class FileExistsException : Exception
	{
		public FileExistsException(string fileName) : base(fileName) { }
	}
}