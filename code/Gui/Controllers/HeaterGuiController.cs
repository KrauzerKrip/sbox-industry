using Sandbox.Exceptions;
using Sandbox.Machines;
using Sandbox.Machines.EveryMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Gui.Controllers
{
	public class HeaterGuiController : MachineGuiController
	{
		private Heater _currentHeater;

		[Property]
		public HeaterGui HeaterGui { get; set; }

		protected override void ShowGuiForMachine( GameObject machineObject )
		{
			Heater heater = machineObject.Components.Get<Heater>();
			if ( heater == null )
			{
				throw new ComponentException( "Heater " + machineObject.Name + " doesn't have Heater component." );
			}

			_currentHeater = heater;

			HeaterGui.Show = true;
		}
	}
}
