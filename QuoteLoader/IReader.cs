using System;

namespace QuoteLoader
{
	public interface IReader : IDisposable
	{
		string[] Read();
	}
}