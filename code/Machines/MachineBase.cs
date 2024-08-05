using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Machines
{
	public class MachineBase : Component
	{
		public GameObject Owner { get; set; }
		public string Name { get; set; }
	}
}
