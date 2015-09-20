using System;
using System.Collections.Generic;
using Quotes;

namespace QuoteLoader.Tests
{
	internal class FakeQuoteRepository : IQuoteRepository
	{
		internal int Count = 0;
		public List<Quote> Data = new List<Quote>();

		public void AddQuote(Quote quote)
		{
			quote.Id = ++Count;
			Data.Add(quote);
		}

		public IEnumerable<Quote> GetQuotes(DateTime start, DateTime end)
		{
			return Data;
		}
	}
}
