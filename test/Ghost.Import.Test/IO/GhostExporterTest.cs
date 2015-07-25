using Ghost.Import.IO;
using Moq;
using NUnit.Framework;

namespace Ghost.Import.Test.IO
{
	[TestFixture]
	class GhostExporterTest
	{
		private const string ExportPath = "export.json";

		[Test]
		public void Export_WhenExporting_ThenShouldCallIFormatterProcess()
		{
			var formatter = new Mock<IFormatter>();
			var import = new Import();
			var exporter = CreateExporter(formatter.Object);

			exporter.Export(import, ExportPath);

			formatter.Verify(f => f.Process(import));
		}

		[Test]
		public void Export_WhenExporting_ThenShouldCallIFileOperationSave()
		{
			var file = new Mock<IFileOperation>();
			var import = new Import();
			var exporter = CreateExporter(fileOperation: file.Object);

			exporter.Export(import, ExportPath);

			file.Verify(f => f.Save(It.IsAny<string>(), ExportPath));
		}

		[Test]
		public void Export_WhenExporting_ThenShouldSaveFormatterResultToExport()
		{
			const string json = "{}";
			var formatter = new Mock<IFormatter>();
			formatter.Setup(m => m.Process(It.IsAny<Import>())).Returns(json);

			var file = new Mock<IFileOperation>();
			var import = new Import();
			var exporter = CreateExporter(formatter.Object, file.Object);

			exporter.Export(import, ExportPath);

			file.Verify(f => f.Save(json, ExportPath));
		}

		private static GhostExporter CreateExporter(IFormatter formatter = null, IFileOperation fileOperation = null)
		{
			return new GhostExporter(formatter ?? new Mock<IFormatter>().Object, fileOperation ?? new Mock<IFileOperation>().Object);
		}
	}
}
