using QuoteLoader.CSV;
using QuoteLoader.Formatters;
using QuoteLoader.Helpers;

using Quotes;

namespace QuoteLoader
{
	public class QuoteImporter : QuoteImporterBase
	{
		public QuoteImporter(IQuoteRepository quoteRepository)
			: base(quoteRepository)
		{			
		}

		public void Import(string inputFileName)
		{
			inputFileName.ThrowIfNull("inputFileName");

			using (var csv = new CsvReader(inputFileName))
			{
				base.Import(csv, new QuoteFormatter());
			}
		}
	}
}
