using System;



namespace QuoteLoader.Formatters
{
	public class QuoteParsingException : Exception
	{
		public Type ExpectedType { get; private set; }
			
		public string RawData { get; private set; }

		public QuoteParsingException(Type expectedType, string rawData, Exception ex)
			: base(string.Empty, ex)
		{
			ExpectedType = expectedType;
			RawData = rawData;			
		}

		public override string Message { get { return string.Format("Can't parse a string '{0}' to type '{1}'.", RawData, ExpectedType.Name); } }		
	}
}
