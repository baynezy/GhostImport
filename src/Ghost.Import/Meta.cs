using System;

namespace Ghost.Import
{
	public class Meta
	{
		public Meta()
		{
			Version = "003";
			ExportedOn = DateTime.Now;
		}
		public DateTime ExportedOn { get; set; }
		public string Version { get; private set; }
	}
}
