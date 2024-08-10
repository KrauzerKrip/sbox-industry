using Sandbox;
using System.Transactions;

namespace Sandbox.Machines
{
	public sealed class Recipe : Component
	{
		[Property]
		public Dictionary<string, float> Ingredients { get; set; }

		public bool IsInventorySufficient(Inventory inventory)
		{
			foreach (var (res, mass) in Ingredients)
			{
				if (inventory.Resources.TryGetValue(res, out float requiredMass))
				{
					if ( mass <= requiredMass ) {
						return false;
					}
				} else
				{
					return false;
				}
			}

			return true;
		}

		protected override void OnUpdate()
		{
			
		}
	}
}
