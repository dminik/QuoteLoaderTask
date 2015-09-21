using System;

namespace QuoteLoader
{
	public interface IWriter : IDisposable
	{
		void Write(string[] data);
		void Close();
	}
}