using Sandbox.Exceptions;
using Sandbox.Machines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class PlayerController
{
	public void TryUseBlueprint()
	{
		SceneTraceResult result = Scene.Trace.Ray( AimRay, 1000 ).HitTriggers().WithTag( "blueprint" ).Run();
		if ( result.GameObject != null && result.GameObject.IsValid )
		{
			Blueprint blueprint = result.GameObject.Components.Get<Blueprint>();
			if ( blueprint == null )
			{
				throw new ComponentException( "Blueprint " + result.GameObject.Name + " doesn't have Blueprint component." );
			}

			blueprint.Use( GameObject );
		}
	}
}
