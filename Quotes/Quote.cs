using System;

namespace Quotes
{
    public class Quote
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }
}
