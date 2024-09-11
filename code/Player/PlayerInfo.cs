using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Player
{
	public class PlayerInfo : Component
	{
		[Property, Category("Info")]
		public Guid Guid { get; set; }
	}
}
