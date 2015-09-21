using System;
using QuoteLoader.CSV;
using Quotes;

namespace QuoteLoader
{	
	public class QuoteImporter
	{
		private readonly IQuoteRepository _quoteRepository;
		private IQuoteParser _parser;
				
		public QuoteImporter(IQuoteRepository quoteRepository)
		{
			_quoteRepository = quoteRepository;
		}

		public IQuoteParser Parser
		{	        
			set { _parser = value; }
		}

		[Obsolete("This method is obsolete; use method public Import(ICsvReader reader) instead")]
		public void Import(string inputFileName)
		{
			if(_parser == null)
				_parser = new QuoteParser();

			using (var csv = new CsvReader(inputFileName))
			{		        		        
				DoImport(csv);
			}
		}

		public void Import(IReader reader, IQuoteParser parser)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			if (parser == null)
				throw new ArgumentNullException("parser");

			_parser = parser;
			DoImport(reader);         
		}

		private void DoImport(IReader reader)
		{
			string[] values;
			int lineNumber = 0;

			while ((values = reader.Read()) != null)
			{
				lineNumber++;

				var item = _parser.Parse(values, lineNumber);
				_quoteRepository.AddQuote(item);
			}
		}
	}
}
