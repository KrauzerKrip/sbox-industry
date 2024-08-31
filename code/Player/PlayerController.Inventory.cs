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
}
