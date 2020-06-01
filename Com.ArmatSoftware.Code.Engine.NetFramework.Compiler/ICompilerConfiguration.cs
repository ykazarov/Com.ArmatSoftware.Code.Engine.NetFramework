using System;
using System.Collections.Generic;
using Com.ArmatSoftware.Code.Engine.NetFramework.Core;

namespace Com.ArmatSoftware.Code.Engine.NetFramework.Compiler
{
	/// <summary>
	/// Compiler configuration
	/// </summary>
	/// <typeparam name="S">Subject type</typeparam>
	public interface ICompilerConfiguration<S> : ITemplateConfiguration where S : class
	{
		/// <summary>
		/// List of actions to compile
		/// </summary>
		IList<IAction<S>> Actions { get; set; }

		/// <summary>
		/// List of imports to add to the template
		/// </summary>
		IList<Type> References { get; set; }
	}
}