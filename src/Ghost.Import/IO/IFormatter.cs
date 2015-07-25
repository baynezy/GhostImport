namespace Ghost.Import.IO
{
	public interface IFormatter
	{
		string Process(Import import);
	}
}
