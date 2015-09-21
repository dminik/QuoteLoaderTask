using System;
using System.Globalization;
using System.Linq;

using Quotes;

namespace QuoteLoader.Formatters
{
	public class QuoteFormatter : IQuoteFormatter
	{
		public Quote FromString(string[] values, int lineNumber)
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

		public string[] ToString(Quote quote)
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
