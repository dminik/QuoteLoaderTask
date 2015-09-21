using System;

namespace QuoteLoader.StorageProviders
{
	public interface IWriter : IDisposable
	{
		void Write(string[] data);
		void Close();
	}
}