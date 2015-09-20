using QuoteLoader.CSV;
using Quotes;
using System;
using System.Globalization;

namespace QuoteLoader
{
    public class QuoteImporter
    {
        private IQuoteRepository _quoteRepository;

        public QuoteImporter(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public void Import(string inputFileName)
        {
            var csv = new CsvReaderWriter();
            csv.Open(inputFileName, false);

            var date = ""; var ticker = ""; var value = "";
            while (csv.Read(out date, out ticker, out value))
            {
                var quote = new Quote();
                quote.DateTime = DateTime.ParseExact(date, "s", CultureInfo.InvariantCulture);
                quote.Ticker = ticker;
                quote.Value = double.Parse(value, CultureInfo.InvariantCulture);
                _quoteRepository.AddQuote(quote);
            }
        }
    }
}
