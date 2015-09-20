using QuoteLoader.CSV;
using Quotes;
using System;
using System.Globalization;

namespace QuoteLoader
{
    public class QuoteExporter
    {
        private IQuoteRepository _quoteRepository;

        public QuoteExporter(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public void Export(string exportFileName, DateTime start, DateTime end)
        {
            var quotes = _quoteRepository.GetQuotes(start, end);

            var csv = new CsvReaderWriter();
            csv.Open(exportFileName, true);

            foreach (var quote in quotes)
            {
                var data = new string[] {
                    quote.Id.ToString(),
                    quote.DateTime.ToString("s", CultureInfo.InvariantCulture),
                    quote.Ticker,
                    quote.Value.ToString("F2", CultureInfo.InvariantCulture),
                };
                csv.Write(data);
            }

            csv.Close();
        }
    }
}
