using Sandbox.Exceptions;
using Sandbox.Machines;
using Sandbox.Machines.EveryMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlayerController;

namespace Sandbox.Gui.Controllers
{
	public class HeaterGuiController : MachineGuiController
	{
		private Heater _currentHeater;

		[Property]
		public HeaterGui HeaterGui { get; set; }

		protected override void ShowMachineGui( GameObject user, GameObject machine )
		{
			PlayerController playerController = user.Components.Get<PlayerController>();
			if (playerController == null)
			{
				throw new ComponentException( "User " + user.Name + " doesn't have PlayerController component." );
			}

			Heater heater = machine.Components.Get<Heater>();
			if ( heater == null )
			{
				throw new ComponentException( "Heater " + machine.Name + " doesn't have Heater component." );
			}

			_currentHeater = heater;

			HeaterGui.Show = true;
			playerController.IsFreezed = true;
			playerController.IsMachineGuiOpened = true;

			playerController.OnHideMachineGui += () =>
			{
				HideMachineGui( user, machine );
			};

			IsShown = true;
		}

		protected override void HideMachineGui( GameObject user, GameObject machine )
		{
			PlayerController playerController = user.Components.Get<PlayerController>();
			if ( playerController == null )
			{
				throw new ComponentException( "User " + user.Name + " doesn't have PlayerController component." );
			}

			_currentHeater = null;

			HeaterGui.Show = false;
			playerController.IsFreezed = false;
			playerController.IsMachineGuiOpened = false;

			IsShown = false;
		}

		protected override void OnUpdate()
		{
			

			base.OnUpdate();
		}
	}
}
