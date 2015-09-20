namespace QuoteLoader.CSV
{
	using System;

	public interface ICsvReader : IDisposable
	{
		bool Read(out string[] values);
	}
}