using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Machines.EveryMachine
{
	public class Pot : MachineBase
	{
		[Property, Category( "Components" )]
		public Inventory OutputInventory { get; private set; }
		[Property, Category("Components")]
		public ResourceConnector HeatIn {  get; private set; }
		public float Temperature { get; private set; }

		protected override void OnStart()
		{
			IsAttachment = true;
			base.OnAwake();
		}

		protected override void OnFixedUpdate()
		{


			base.OnFixedUpdate();
		}
	}
}
