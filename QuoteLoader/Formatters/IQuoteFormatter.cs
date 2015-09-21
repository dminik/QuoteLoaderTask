using Quotes;

namespace QuoteLoader.Formatters
{
	public interface IQuoteFormatter
	{
		Quote FromString(string[] values, int lineNumber);
		string[] ToString(Quote quote);
	}
}