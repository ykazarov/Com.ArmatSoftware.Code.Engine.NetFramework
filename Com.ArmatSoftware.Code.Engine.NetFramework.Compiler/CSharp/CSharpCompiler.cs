using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Com.ArmatSoftware.Code.Engine.NetFramework.Core;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualStudio.TextTemplating;

namespace Com.ArmatSoftware.Code.Engine.NetFramework.Compiler.CSharp
{
	/// <summary>
	/// Default implementation for the C# compiler
	/// </summary>
	/// <typeparam name="S">Subject type</typeparam>
	public class CSharpCompiler<S> : ICompiler<S> where S : class
	{
		public const string CSharpCodeProviderName = "CSharp";

		/// <summary>
		/// Compile the actions using C# compiler.
		/// Important: Essential references for subject and executor types are added to the configuration automagically
		/// </summary>
		/// <param name="configuration">Compiler configuration</param>
		/// <returns>Executor object</returns>
		public IExecutor<S> Compile(ICompilerConfiguration<S> configuration)
		{
			ValidateConfiguration(configuration);

			AddTemplateReferences(configuration);

			// generate code
			var codeGenerator = new CSharpExecutorTemplate(configuration);

			var session = new TextTemplatingSession();
			codeGenerator.Session = session;
			codeGenerator.Initialize();

			var code = codeGenerator.TransformText();

			var fullClassName = $"{configuration.GetNamespace()}.{configuration.GetClassName()}";

			CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");

			CompilerParameters parameters = new CompilerParameters
			{
				GenerateExecutable = false,
				CompilerOptions = "/t:library"
			};

			foreach(var reference in configuration.References)
			{
				var referenceAssembly = reference.Assembly.GetName().Name;
				if(!parameters.ReferencedAssemblies.Contains(referenceAssembly))
				{
					parameters.ReferencedAssemblies.Add(referenceAssembly);
				}
			}

			var results = codeProvider.CompileAssemblyFromSource(parameters, code);
			ValidateCompilationResults(results);

			var executorInstanceHandle = Activator.CreateInstance(results.CompiledAssembly.FullName, fullClassName);

			return (IExecutor<S>)executorInstanceHandle.Unwrap();
		}

		private static void ValidateCompilationResults(CompilerResults results)
		{
			if (results.Errors.HasErrors)
			{
				var errors = new StringBuilder();
				errors.AppendLine(string.Join("\r\n", results.Errors.Cast<CompilerError>().Select(e => e.ErrorText).ToList()));
				Debug.WriteLine(errors);
				throw new InvalidOperationException(errors.ToString());
			}
		}

		/// <summary>
		/// Add template specific type references
		/// </summary>
		/// <param name="configuration"></param>
		private static void AddTemplateReferences(ICompilerConfiguration<S> configuration)
		{
			configuration.References.Add(typeof(Dictionary<,>));
			configuration.References.Add(typeof(S));
			configuration.References.Add(typeof(IExecutor<>));
			configuration.References.Add(typeof(DynamicAttribute));
			configuration.References.Add(typeof(RuntimeBinderException));

		}

		private static void ValidateConfiguration(ICompilerConfiguration<S> configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			if (configuration.Actions == null || !configuration.Actions.Any())
			{
				throw new ArgumentException("Actions are null or empty", nameof(configuration.Actions));
			}

			if (configuration.References == null)
			{
				throw new ArgumentNullException(nameof(configuration.References));
			}
		}

	}
}
