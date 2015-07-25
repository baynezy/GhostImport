using System;
using NUnit.Framework;
using Ghost.Import.IO;

namespace Ghost.Import.Test.IO
{
	[TestFixture]
	class EpochTimeTest
	{
		[Test]
		public void EpochTime_ImplementsIEpochTime()
		{
			var epochConverter = CreateEpochTime();

			Assert.That(epochConverter, Is.InstanceOf<IEpochTime>());
		}
		
		[Test]
		public void ConvertTo_WhenPassingInDateTime_ThenReturnEpochTime001()
		{
			var epochConverter = CreateEpochTime();
			var christmas = new DateTime(2014, 12, 25, 12, 0, 0);
			const long epoch = 1419508800000;

			Assert.That(epochConverter.ConvertTo(christmas), Is.EqualTo(epoch));
		}

		[Test]
		public void ConvertTo_WhenPassingInDateTime_ThenReturnEpochTime002()
		{
			var epochConverter = CreateEpochTime();
			var birtday = new DateTime(2014, 12, 24, 12, 0, 0);
			const long epoch = 1419422400000;

			Assert.That(epochConverter.ConvertTo(birtday), Is.EqualTo(epoch));
		}

		private static EpochTime CreateEpochTime()
		{
			return new EpochTime();
		}
	}
}
