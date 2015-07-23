using System;

namespace Ghost.Import.IO
{
	public class EpochTime : IEpochTime
	{
		public long ConvertTo(DateTime dateTime)
		{
			var tstamp = dateTime - new DateTime(1970, 1, 1);
			var millisecondsSinceEpoch = (long) tstamp.TotalMilliseconds;
			return millisecondsSinceEpoch;
		}
	}
}
