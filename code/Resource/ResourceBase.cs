using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum State
{
	Solid,
	Liquid,
	Gas
}

public class ResourceBase : Component
{
	[Property, Category( "Info" )]
	public string Name { get; protected set; }
	[Property, Category( "Info" )]
	public State State { get; protected set; }
	
}
