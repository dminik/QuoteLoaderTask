using System;
using System.Collections.Generic;
using Quotes;

namespace QuoteLoader.Tests
{
	internal class FakeQuoteRepository : IQuoteRepository
	{
		internal int Count = 0;
		private List<Quote> _data = new List<Quote>();

		public void AddQuote(Quote quote)
		{
			quote.Id = ++Count;
			_data.Add(quote);
		}

		public IEnumerable<Quote> GetQuotes(DateTime start, DateTime end)
		{
			return _data;
		}
	}
}
