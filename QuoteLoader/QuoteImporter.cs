using System;
using System.Globalization;
using System.Linq;
using QuoteLoader.CSV;
using Quotes;

namespace QuoteLoader
{
	using System.IO;

	public class QuoteImporter
	{
		private readonly IQuoteRepository _quoteRepository;

		public QuoteImporter(IQuoteRepository quoteRepository)
		{
			_quoteRepository = quoteRepository;
		}

		public void Import(string inputFileName)
		{
			using (var csv = new CsvReader(inputFileName))
			{		        		        
				DoImport(csv);
			}
		}

		internal void Import(StreamReader stream)
		{
			using (var csv = new CsvReader(stream))
			{
				DoImport(csv);
			}
		}

		private void DoImport(CsvReader csv)
		{
			string[] values;
			int lineNumber = 0;

			while (csv.Read(out values))
			{
				lineNumber++;

				if (!values.Any()) // skip empty line
				{
					continue;
				}

				var item = Parse(values, lineNumber);
				_quoteRepository.AddQuote(item);
			}
		}

		private Quote Parse(string[] values, int lineNumber)
		{
			const int FIELD_NUMBER = 3;

			if (values.Count() != FIELD_NUMBER)
			{
				var str = string.Join(", ", values);
				throw new FormatException(
					string.Format(
						"Wrong fields number in line {0}. Expected {1} but was found {2}. Fields: {3}",
						lineNumber,
						FIELD_NUMBER,
						values.Count(),
						str));
			}

			var quote = new Quote();
			
			try
			{				
				quote.DateTime = DateTime.ParseExact(values[0].Trim(), "s", CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				throw new FormatException(string.Format("Can't parse field 'DateTime' from string '{0}' to type 'DateTime'.", values[0]), ex);				
			}
			
			quote.Ticker = values[1];

			try
			{
				quote.ValueExact = decimal.Parse(values[2], CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				throw new FormatException(string.Format("Can't parse field 'Value' from string '{0}' to type 'double'.", values[2]), ex);
			}
			
			return quote;
		}
	}
}
