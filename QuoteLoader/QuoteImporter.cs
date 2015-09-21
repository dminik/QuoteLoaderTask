using System;
using QuoteLoader.CSV;
using QuoteLoader.Formatters;

using Quotes;

namespace QuoteLoader
{	
	public class QuoteImporter
	{
		private readonly IQuoteRepository _quoteRepository;
		private IQuoteFormatter _formatter;
				
		public QuoteImporter(IQuoteRepository quoteRepository)
		{
			_quoteRepository = quoteRepository;
		}

		public IQuoteFormatter Formatter
		{	        
			set { _formatter = value; }
		}

		[Obsolete("This method is obsolete; use method public Import(ICsvReader reader) instead")]
		public void Import(string inputFileName)
		{
			if(_formatter == null)
				_formatter = new QuoteFormatter();

			using (var csv = new CsvReader(inputFileName))
			{		        		        
				DoImport(csv);
			}
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
