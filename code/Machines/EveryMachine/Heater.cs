using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Machines.EveryMachine
{
	public class Heater : MachineBase
	{
		public SolidFuel FuelResource { get; private set; }
		public float FuelMass { get; private set; }

		protected override void OnFixedUpdate()
		{
			base.OnFixedUpdate();
		}
	}
}
