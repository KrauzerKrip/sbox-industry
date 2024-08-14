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

		[Property, Category("Info")]
		public SolidFuel FuelResource { get; private set; }
		[Property, Category( "Info" ), Range(0, 10)]
		public float FuelMass { get; private set; }

		protected override void OnFixedUpdate()
		{
			if ( FuelMass <= 0 )
			{
				return;
			}

			float fuelConsumptionPerSecond = 1.0f / FuelResource.BurningDuration;
			float fuelConsumed = fuelConsumptionPerSecond * Time.Delta;
			
			if (FuelMass < fuelConsumed) {
				fuelConsumed = FuelMass;
			}

			float heatProduced = fuelConsumed * FuelResource.CombustionHeat;

			HeatOut.ResourceAmount += heatProduced;
			HeatOut.CurrentResource = "heat";

			FuelMass -= fuelConsumed;

			base.OnFixedUpdate();
		}
	}
}
