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



			GameObject.Destroy();
		}
	}
}
