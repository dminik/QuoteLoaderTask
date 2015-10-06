using System;



namespace QuoteLoader.Formatters
{
	public class WrongFieldsNumberException : Exception
	{
		public uint LineNumber { get; private set; }
		public uint ExpectedNum { get; private set; }
		public uint ActualNum { get; private set; }		
		public string RawData { get; private set; }

		public WrongFieldsNumberException(uint lineNumber, uint expectedNum, uint actualNum, string rawData)
		{
			LineNumber = lineNumber;
			ExpectedNum = expectedNum;
			ActualNum = actualNum;
			RawData = rawData;
		}

		public override string Message
		{
			get
			{
				return string.Format("Wrong fields number in line {0}. Expected {1} but was found {2}. Fields: {3}",
						LineNumber,
						ExpectedNum,
						ActualNum,
						RawData);
			}
			
		}
	}
}
