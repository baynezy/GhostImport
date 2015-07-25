namespace Ghost.Import.IO
{
	public class GhostExporter
	{
		private readonly IFormatter _formatter;
		private readonly IFileOperation _fileOperation;

		public GhostExporter(IFormatter formatter, IFileOperation fileOperation)
		{
			_formatter = formatter;
			_fileOperation = fileOperation;
		}

		public void Export(Import import, string exportPath)
		{
			var contents = _formatter.Process(import);
			_fileOperation.Save(contents, exportPath);
		}
	}
}
