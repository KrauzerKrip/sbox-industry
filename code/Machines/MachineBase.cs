using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Machines
{
	public abstract class MachineBase : Component
	{
		[Property]
		public MachineSubscriber Subscriber { get; set; }

		public GameObject Owner { get; set; }
		public string Name { get; set; }
		public delegate void UseHandler( GameObject user );
		public event UseHandler OnUse;

		protected override void OnStart()
		{
			Subscriber.SubscribeOnMachine(machineObject: GameObject);
			base.OnStart();
		}

		public virtual void Use( GameObject user )
		{
			OnUse( user );
		}
	}
}
