using System;

namespace Quotes
{
	public class Quote
	{
		public int Id { get; set; }
		public string Ticker { get; set; }
		public DateTime DateTime { get; set; }

		[Obsolete("This propery is obsolete; use prperty ValueExact instead")]
		public double Value
		{
			get
			{
				return (double)ValueExact;
			}
			set
			{
				ValueExact = (decimal)value;
			}
		}

		public decimal ValueExact { get; set; }
	}
}
