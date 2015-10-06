using Quotes;

namespace QuoteLoader.Formatters
{
	public interface IQuoteFormatter
	{
		Quote FromString(string[] values, uint lineNumber);
		string[] ToString(Quote quote);
	}
}