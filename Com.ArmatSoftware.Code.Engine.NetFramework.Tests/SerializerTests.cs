using System.Collections.Generic;
using Com.ArmatSoftware.Code.Engine.NetFramework.Serializer;

namespace Com.ArmatSoftware.Code.Engine.NetFramework.Tests
{
	public class SerializerTests
	{
		public class JsonSerializerTests
		{
		}

		public class XmlSerializerTests
		{
		}

		private class SerializerTestBase
		{
			public IEnumerable<TestAction> Actions { get; }
			public XmlSerializer XmlSerializer { get; }
			public JsonSerializer JsonSerializer { get; }

			public SerializerTestBase()
			{
				XmlSerializer = new XmlSerializer();
				JsonSerializer = new JsonSerializer();
				Actions = new List<TestAction>
				{
					new TestAction { Name = "uno", Code = "var uno = 1;" },
					new TestAction { Name = "dos", Code = "var dos = 2;" },
					new TestAction { Name = "tres", Code = "var tres = 3;" },
					new TestAction { Name = "quatro", Code = "var quatro = 4;" }
				};
			}
		}
	}
}
