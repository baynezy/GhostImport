using System.IO;
using System.Text;

namespace Ghost.Import.IO
{
	class FileOperation : IFileOperation
	{
		public void Save(string contents, string filePath)
		{
			File.WriteAllText(filePath, contents, Encoding.UTF8);
		}
	}
}
