using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Machines
{
	public sealed class ResourceConnection : Component
	{
		[Property]
		public ResourceConnector ResourceConnectorIn { get; set; }
		[Property]
		public ResourceConnector ResourceConnectorOut { get; set; }

	}
}
