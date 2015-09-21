using System;
using System.Globalization;
using QuoteLoader.CSV;
using Quotes;

namespace QuoteLoader
{
	public class QuoteExporter
	{
		private readonly IQuoteRepository _quoteRepository;

		public QuoteExporter(IQuoteRepository quoteRepository)
		{
			_quoteRepository = quoteRepository;
		}

		public void Export(string exportFileName, DateTime start, DateTime end)
		{
			var quotes = _quoteRepository.GetQuotes(start, end);

			using (var csv = new CsvWriter(exportFileName))
			{
				foreach (var quote in quotes)
				{
					var data = ToArray(quote);
					csv.Write(data);
				}
			}
		}

		private static string[] ToArray(Quote quote)
		{
			var data = new string[]
			{				
				quote.DateTime.ToString("s", CultureInfo.InvariantCulture),
				quote.Ticker,
				quote.ValueExact.ToString("F2", CultureInfo.InvariantCulture),
			};

			return data;
		}
	}
}
