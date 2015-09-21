using System;

namespace Quotes
{
	public class Quote
	{
		public int Id { get; set; }
		public string Ticker { get; set; }
		public DateTime DateTime { get; set; }

		[Obsolete("This propery is obsolete; use property ValueExact instead")]
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

		public override bool Equals(object obj)
		{			
			if (obj == null)
			{
				return false;
			}

			Quote p = obj as Quote;
			if ((System.Object)p == null)
			{
				return false;
			}
			
			return (Id == p.Id) 
				&& (Ticker == p.Ticker) 
				&& (DateTime == p.DateTime) 
				&& (ValueExact == p.ValueExact);
		}

		public override int GetHashCode()
		{
			return Id ^ (int)DateTime.Ticks ^ (int) ValueExact;
		}
	}
}
