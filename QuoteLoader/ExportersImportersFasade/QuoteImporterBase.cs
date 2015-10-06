
using QuoteLoader.Formatters;
using QuoteLoader.Helpers;
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
			quoteRepository.ThrowIfNull("quoteRepository");
			_quoteRepository = quoteRepository;
		}
		
		public void Import(IReader reader, IQuoteFormatter formatter)
		{
			reader.ThrowIfNull("reader");
			formatter.ThrowIfNull("formatter");
			
			_formatter = formatter;
			DoImport(reader);         
		}

		private void DoImport(IReader reader)
		{
			string[] values;
			uint lineNumber = 0;

			while ((values = reader.Read()) != null)
			{
				lineNumber++;

				if (string.Join(string.Empty, values).Trim() == string.Empty) // skip empty line
					continue;
				
				var item = _formatter.FromString(values, lineNumber);
				_quoteRepository.AddQuote(item);
			}
		}
	}
}
