namespace Ghost.Import.IO
{
	public interface IFileOperation
	{
		void Save(string contents, string filePath);
	}
}
