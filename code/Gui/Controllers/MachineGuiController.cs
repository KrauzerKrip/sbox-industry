﻿using Sandbox.Exceptions;
using Sandbox.Machines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Gui.Controllers
{
	public abstract class MachineGuiController : MachineSubscriber
	{
		public MachineGuiController()
		{
		}

		public override void SubscribeOnMachine( GameObject machineObject )
		{
			MachineBase machineComponent = machineObject.Components.Get<MachineBase>();
			if ( machineComponent == null )
			{ 
				throw new ComponentException( "Machine " + machineObject.Name + " doesn't have MachineBase component." );
			}

			machineComponent.OnUse += ShowGuiForMachine;
		}

		protected abstract void ShowGuiForMachine( GameObject machineObject );
	}
}
