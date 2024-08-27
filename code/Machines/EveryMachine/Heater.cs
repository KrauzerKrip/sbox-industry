using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Machines.EveryMachine
{
	public class Heater : MachineBase
	{
		[Property, Category( "Components" )]
		public ResourceConnector HeatOut {  get; set; }
		[Property, Category( "Components" )]
		public Resources Resources { get; set; }

		[Property, Category("Info")]
		public string FuelResourceName { get; private set; }

		public override float TryLoadResource( string name, float mass )
		{
			float loaded = base.TryLoadResource( name, mass );
			if ( loaded > 0 )
			{
				FuelResourceName = name;
			}

			return loaded;
		}

		public float GetFuelMass()
		{
			if (FuelResourceName == null)
			{
				return 0;
			}

			if (Inventory.Resources.TryGetValue(FuelResourceName, out float mass))
			{
				return mass;
			}
			return 0;
		}

		protected override void OnStart()
		{
			AcceptableResources = new List<string>() {
				"wood"
			};

			base.OnStart();
		}

		protected override void OnFixedUpdate()
		{
			float fuelMass = GetFuelMass();

			if ( fuelMass <= 0 )
			{
				return;
			}

			Resources resources = Resources.Components.
				Get<Resources>();
			GameObject fuelResourceObject = resources.GetByName( "wood" );
			SolidFuel fuelResource = fuelResourceObject.Components.Get<SolidFuel>();

			float fuelConsumptionPerSecond = 1.0f / fuelResource.BurningDuration;
			float fuelConsumed = fuelConsumptionPerSecond * Time.Delta;

			if ( fuelMass < fuelConsumed )
			{
				fuelConsumed = fuelMass;
			}

			float heatProduced = fuelConsumed * fuelResource.CombustionHeat;

			HeatOut.ResourceAmount += heatProduced;
			HeatOut.CurrentResource = "heat";

			Inventory.Resources[FuelResourceName] -= fuelConsumed;

			base.OnFixedUpdate();
		}
	}
}
