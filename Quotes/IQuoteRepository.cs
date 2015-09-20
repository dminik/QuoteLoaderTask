using System;
using System.Collections.Generic;

namespace Quotes
{
    public interface IQuoteRepository
    {
        void AddQuote(Quote quote);
        IEnumerable<Quote> GetQuotes(DateTime start, DateTime end);
    }
}
