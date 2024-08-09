using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Machines
{
	public abstract class MachineSubscriber : Component
	{
		public abstract void SubscribeOnMachine( GameObject machineObject );
	}
}
