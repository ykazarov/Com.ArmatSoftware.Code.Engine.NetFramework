using System.Collections.Generic;
using Com.ArmatSoftware.Code.Engine.NetFramework.Core;

namespace Com.ArmatSoftware.Code.Engine.NetFramework.Serializer
{
	public interface ISerializer
	{
		IEnumerable<IAction<S>> Deserialize<S>(string actionsJson) where S : class;

		string Serialize<S>(IEnumerable<IAction<S>> actions) where S : class;
	}
}