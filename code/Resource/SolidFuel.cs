using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SolidFuel : Component
{
	[Property]
	public float CombustionHeat { get; set; }
	[Property]
	public float BurningDuration { get; set; }
}
