using System;
using System.Globalization;
using System.Linq;

using QuoteLoader.Formatters.Exceptions;
using QuoteLoader.Helpers;

using Quotes;

namespace QuoteLoader.Formatters
{
	public class QuoteFormatter : IQuoteFormatter
	{
		public Quote FromString(string[] values, uint lineNumber)
		{
			values.ThrowIfNull("values");

			const int EXPECTED_FIELD_NUMBER = 3;

			const int INDEX_DATETIME = 0;
			const int INDEX_TICKER = 1;
			const int INDEX_VALUE = 2;

			if (values.Count() != EXPECTED_FIELD_NUMBER)
			{
				var str = string.Join(", ", values);
				throw new WrongFieldsNumberException(lineNumber, EXPECTED_FIELD_NUMBER, (uint) values.Count(), str);
			}

			var quote = new Quote();

			try
			{
				quote.DateTime = DateTime.ParseExact(values[INDEX_DATETIME].Trim(), "s", CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				throw new QuoteParsingException(typeof(DateTime), values[INDEX_DATETIME], ex);
			}

			quote.Ticker = values[INDEX_TICKER];

			try
			{
				quote.ValueExact = decimal.Parse(values[INDEX_VALUE], CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				throw new QuoteParsingException(typeof(double), values[INDEX_VALUE], ex);				
			}

			return quote;
		}

		public string[] ToString(Quote quote)
		{
			quote.ThrowIfNull("quote");

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
