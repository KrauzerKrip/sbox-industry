using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Machines
{
	public abstract class TraderSubscriber : Component
	{
		public abstract void SubscribeOnTrader( GameObject machineObject );
	}
}
