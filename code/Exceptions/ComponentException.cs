using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Exceptions
{
	public class ComponentException : Exception
	{
		public ComponentException( string message ) : base( message ) { }
		public ComponentException( string message, Exception innerException ) : base( message, innerException ) { }
	}
}
