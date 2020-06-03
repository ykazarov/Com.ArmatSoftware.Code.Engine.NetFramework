using System.Collections.Generic;
using Com.ArmatSoftware.Code.Engine.NetFramework.Core;

namespace Com.ArmatSoftware.Code.Engine.NetFramework.Serializer
{
	public class XmlSerializer : ISerializer
	{
		private XmlSerializer serializer = new XmlSerializer();

		public IEnumerable<IAction<S>> Deserialize<S>(string actionsJson) where S : class
		{
			return serializer.Deserialize<S>(actionsJson);
		}

		public string Serialize<S>(IEnumerable<IAction<S>> actions) where S : class
		{
			return serializer.Serialize<S>(actions);
		}
	}
}
