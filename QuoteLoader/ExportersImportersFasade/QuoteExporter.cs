using System;
using QuoteLoader.CSV;
using QuoteLoader.Formatters;
using QuoteLoader.Helpers;
using QuoteLoader.StorageProviders;

using Quotes;

namespace QuoteLoader
{
	public class QuoteExporter
	{
		private readonly IQuoteRepository _quoteRepository;
		private IQuoteFormatter _formatter;

		public QuoteExporter(IQuoteRepository quoteRepository)
		{
			quoteRepository.ThrowIfNull("quoteRepository");
			_quoteRepository = quoteRepository;
		}

		public IQuoteFormatter Formatter
		{
			set
			{
				value.ThrowIfNull("value"); 
				_formatter = value;
			}
		}

		[Obsolete("This method is obsolete; use method public Export(string exportFileName, DateTime start, DateTime end) instead")]
		public void Export(string exportFileName, DateTime start, DateTime end)
		{
			exportFileName.ThrowIfNull("exportFileName");

			_formatter = new QuoteFormatter();

			using (var writer = new CsvWriter(exportFileName))
			{
				DoExport(writer, start, end);
			}			
		}

		public void Export(IWriter writer, IQuoteFormatter formatter, DateTime start, DateTime end)
		{
			writer.ThrowIfNull("writer");
			formatter.ThrowIfNull("formatter");

			_formatter = formatter;
			DoExport(writer, start, end);
		}

		private void DoExport(IWriter writer, DateTime start, DateTime end)
		{			
			var quotes = _quoteRepository.GetQuotes(start, end);
			
			foreach (var quote in quotes)
			{
				var data = _formatter.ToString(quote);
				writer.Write(data);
			}			
		}
	}
}
