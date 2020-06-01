using Com.ArmatSoftware.Code.Engine.NetFramework.Core;

namespace Com.ArmatSoftware.Code.Engine.NetFramework.Tests
{
	public class TestAction : IAction<TestSubject>
	{
		public string Name { get; set; }

		public string Code { get; set; }
	}
}
