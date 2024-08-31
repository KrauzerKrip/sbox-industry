using Sandbox.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Machines;
using Sandbox.Player;

public partial class PlayerController
{
	public delegate void HideMachineGuiHandler( );
	public event HideMachineGuiHandler OnHideMachineGui;
	public bool IsMachineGuiOpened { get; set; }

	public void TryUse()
	{
		SceneTraceResult result = Scene.Trace.Ray( AimRay, 1000 ).WithTag( "usable" ).Run();
		if ( result.GameObject != null && result.GameObject.IsValid )
		{
			IUsable usable = result.GameObject.Components.Get<IUsable>();
			if ( usable == null )
			{
				throw new ComponentException("Usable game object " +  result.GameObject.Name + " doesn't have a component implement IUsable.");
			}

			usable.Use( GameObject );
		}
	}
}
