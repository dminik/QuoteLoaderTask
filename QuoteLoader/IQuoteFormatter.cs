using Quotes;

namespace QuoteLoader
{
	public interface IQuoteFormatter
	{
		Quote FromString(string[] values, int lineNumber);
		string[] ToString(Quote quote);
	}
}