using System.Collections.Generic;
using Com.ArmatSoftware.Code.Engine.NetFramework.Core;

namespace Com.ArmatSoftware.Code.Engine.NetFramework.Serializer
{
	public class JsonSerializer : ISerializer
	{
		private JsonSerializer serializer = new JsonSerializer();

		public IEnumerable<IAction<S>> Deserialize<S>(string actionsJson) where S : class
		{
			return serializer.Deserialize<S>(actionsJson);
		}

		public string Serialize<S>(IEnumerable<IAction<S>> actions) where S : class
		{
			return serializer.Serialize(actions);
		}
	}
}
