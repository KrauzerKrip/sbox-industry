using Sandbox.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Exceptions;

namespace Sandbox.Agriculture
{
	public class Bush : Component, IUsable, IHarvestable
	{
		[Property]
		public GameObject ResourceCrate { get; set; }

		private const float _yieldMass = 1;

		public void Use( GameObject user )
		{

		}

		public void Harvest( GameObject harvester )
		{
			Inventory inventory = harvester.Components.Get<Inventory>();
			if (inventory == null )
			{
				throw new ComponentException( "Harvester doesn't have Inventory component" );
			}

			float loaded = inventory.TryLoadResource( "wood", _yieldMass );
			float rest = _yieldMass - loaded;

			if ( rest > 0 )
			{
				GameObject resourceCrateObject = ResourceCrate.Clone();
				resourceCrateObject.Transform.Position = GameObject.Transform.Position;
				ResourceCrate resourceCrate = resourceCrateObject.Components.Get<ResourceCrate>();
				if (resourceCrate == null)
				{
					throw new ComponentException( "ResourceCrate object doesn't have ResourceCrate component" );
				}
				resourceCrate.ResourceName = "wood";
				resourceCrate.ResourceMass = rest;
			}

			GameObject.Destroy();
		}
	}
}
