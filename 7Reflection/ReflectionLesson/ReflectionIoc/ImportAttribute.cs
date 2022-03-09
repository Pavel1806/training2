using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class ImportAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class ImportConstructorAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class ExportAttribute : Attribute
	{
		public ExportAttribute()
		{ }

		public ExportAttribute(Type contract)
		{
			Contract = contract;
		}

		public Type Contract { get; private set; }
	}
}
