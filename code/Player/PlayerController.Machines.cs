using Sandbox.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Machines;

public partial class PlayerController
{
	public void TryUseMachine()
	{
		SceneTraceResult result = Scene.Trace.Ray( AimRay, 1000 ).WithTag( "machine" ).Run();
		if ( result.GameObject != null && result.GameObject.IsValid )
		{
			MachineBase machineBase = result.GameObject.Components.Get<MachineBase>();

			if ( machineBase == null )
			{
				throw new ComponentException("Machine " +  result.GameObject.Name + " doesn't have MachineBase component.");
			}

			machineBase.Use( result.GameObject );
		}
	}
}
