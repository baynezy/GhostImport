using System;

namespace Ghost.Import.IO
{
	public interface IEpochTime
	{
		long ConvertTo(DateTime dateTime);
	}
}
