namespace Ghost.Import
{
	public class Import
	{
		public Import()
		{
			Meta = new Meta();
			Data = new Data();
		}
		public Meta Meta { get; set; }
		public Data Data { get; set; }

	}
}