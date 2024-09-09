using Sandbox.Exceptions;
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
		public Dictionary<string, float> PurchaseOffers { get; set; }   // name, price
		[Property, Category( "Info" )]
		public Dictionary<string, float> SaleOffers { get; set; }       // name, price
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

		public abstract void Buy( GameObject buyer, string itemName, float count );

		public abstract void Sell( GameObject seller, string itemName, float count );
	}
}
