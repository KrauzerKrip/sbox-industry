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

		protected override void OnFixedUpdate()
		{
			if (ResourceConnectorIn.ResourceAmount == 0 || ResourceConnectorIn.CurrentResource == ResourceConnectorOut.CurrentResource) {
				ResourceConnectorIn.ResourceAmount += ResourceConnectorOut.ResourceAmount;
				ResourceConnectorIn.CurrentResource = ResourceConnectorOut.CurrentResource;
				ResourceConnectorOut.ResourceAmount = 0;
			}

			base.OnFixedUpdate();
		}

	}
}
