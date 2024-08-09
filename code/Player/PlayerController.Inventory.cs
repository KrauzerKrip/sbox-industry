using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class PlayerController
{
	[Property]
	[Category("Components")]
	public Inventory Inventory { get; set; }

	public bool IsInventoryOpened { get; private set; }

	private void TryLoadResourceIntoInventory()
	{
		SceneTraceResult result = Scene.Trace.Ray( AimRay, 1000 ).WithTag( "resource_crate" ).Run();

		if ( result.GameObject != null && result.GameObject.IsValid )
		{
			ResourceCrate crate = result.GameObject.Components.Get<ResourceCrate>();
			float loadedMass = Inventory.TryLoadResource( crate.Resource, crate.Mass );
			crate.Mass -= loadedMass;
			Log.Info("Loaded " +  loadedMass + " of " + crate.Resource.Name);
		}
	}
}
