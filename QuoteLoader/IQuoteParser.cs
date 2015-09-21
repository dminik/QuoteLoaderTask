using Quotes;

namespace QuoteLoader
{
    public interface IQuoteParser
    {
        Quote Parse(string[] values, int lineNumber);
    }
}