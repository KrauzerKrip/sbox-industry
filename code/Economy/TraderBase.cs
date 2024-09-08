using Sandbox.Machines;
using Sandbox.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Economy
{
	public abstract class TraderBase : Component
	{

		[Property, Category( "Info" )]
		public Dictionary<string, float> Buying { get; set; }
		[Property, Category( "Info" )]
		public Dictionary<string, float> Selling { get; set; }
		[Property, Category( "Components" )]
		public TraderSubscriber Subscriber { get; set; }

		public delegate void UseHandler( GameObject user, GameObject machine );
		public event UseHandler OnUse;

		protected override void OnStart()
		{
			if ( Subscriber != null )
			{
				Subscriber.SubscribeOnTrader( machineObject: GameObject );
			}
			base.OnStart();
		}

		public virtual void Use( GameObject user )
		{
			if ( OnUse != null )
			{
				OnUse( user, GameObject );
			}
		}
	}
}
