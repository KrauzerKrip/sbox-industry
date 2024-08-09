using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ResourceBase : Component
{
	[Property]
	[Category("Info")]
	public string Name { get; set; }

}
