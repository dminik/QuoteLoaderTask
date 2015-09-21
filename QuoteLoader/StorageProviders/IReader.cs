using System;

namespace QuoteLoader.StorageProviders
{
	public interface IReader : IDisposable
	{
		string[] Read();
	}
}