using System.IO;

namespace QuoteLoader.Tests.Helpers
{
	public static class StreamHelper
	{
		public static StreamReader ToStream(this string s)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return new StreamReader(stream);
		}
	}
}
