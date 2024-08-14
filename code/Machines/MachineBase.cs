using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Machines
{
	public abstract class MachineBase : Component
	{
		[Property, Category( "Components" )]
		public MachineSubscriber Subscriber { get; set; }
		[Property, Category( "Components" )]
		public Inventory Inventory { get; set; }
		[Property]
		public bool IsAttachment { get; protected set; }
		[Property]
		public List<ResourceConnector> Connectors { get; set; } = new List<ResourceConnector>();
		[Property]
		public List<string> AcceptableResources { get; set; }
		public bool IsAttached { get; protected set; }
		public GameObject Owner { get; set; }
		public string Name { get; set; }
		public delegate void UseHandler( GameObject user, GameObject machine );
		public event UseHandler OnUse;

		protected override void OnStart()
		{
			if ( Subscriber != null )
			{
				Subscriber.SubscribeOnMachine( machineObject: GameObject );
			}
			base.OnStart();
		}

		public virtual void Use( GameObject user )
		{
			if (OnUse != null)
			{
				OnUse( user, GameObject );
			}
		}

		protected override void OnFixedUpdate()
		{
			if (IsAttachment)
			{
				foreach (ResourceConnector connector in Connectors)
				{
					if (connector.ResourceConnection != null)
					{
						IsAttached = true;
					}
				}
			}

			base.OnFixedUpdate();
		}

		public virtual float TryLoadResource(string name, float mass)
		{
			if (AcceptableResources.Contains(name))
			{
				return Inventory.TryLoadResource(name, mass);
			}
			return 0;
		}
	}
}
