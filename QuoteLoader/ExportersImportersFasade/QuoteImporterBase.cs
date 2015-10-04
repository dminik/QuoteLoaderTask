using System;

using QuoteLoader.Formatters;
using QuoteLoader.StorageProviders;

using Quotes;

namespace QuoteLoader
{	
	public class QuoteImporterBase
	{
		private readonly IQuoteRepository _quoteRepository;
		private IQuoteFormatter _formatter;

		public QuoteImporterBase(IQuoteRepository quoteRepository)
		{
			_quoteRepository = quoteRepository;
		}
		
		public void Import(IReader reader, IQuoteFormatter formatter)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			if (formatter == null)
				throw new ArgumentNullException("formatter");

			_formatter = formatter;
			DoImport(reader);         
		}

		private void DoImport(IReader reader)
		{
			string[] values;
			int lineNumber = 0;

			while ((values = reader.Read()) != null)
			{
				lineNumber++;

				var item = _formatter.FromString(values, lineNumber);
				_quoteRepository.AddQuote(item);
			}
		}
	}
}
