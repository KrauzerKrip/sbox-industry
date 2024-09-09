using Sandbox;
using Sandbox.Player;
using Sandbox.Economy;
using Sandbox.Exceptions;

public sealed class ResourceTrader : TraderBase, IUsable
{
	public override void Buy( GameObject buyer, string itemName, float count )
	{
		Wallet wallet = buyer.Components.Get<Wallet>();
		if ( wallet == null )
		{
			throw new ComponentException( $"The buyer {buyer.Name} doesn't have Wallet component." );
		}
		Inventory inventory = buyer.Components.Get<Inventory>();
		if ( inventory == null )
		{
			throw new ComponentException( $"The buyer {buyer.Name} doesn't have Inventory component." );
		}

		if ( wallet.Balance > count * PurchaseOffers[itemName] )
		{
			wallet.Balance -= count * PurchaseOffers[itemName];
			inventory.ForceLoadResource( itemName, count );
		}
	}

	public override void Sell( GameObject seller, string itemName, float count )
	{
		Wallet wallet = seller.Components.Get<Wallet>();
		if ( wallet == null )
		{
			throw new ComponentException( $"The buyer {seller.Name} doesn't have Wallet component." );
		}
		Inventory inventory = seller.Components.Get<Inventory>();
		if ( inventory == null )
		{
			throw new ComponentException( $"The buyer {seller.Name} doesn't have Inventory component." );
		}

		float unloaded = inventory.TryUnloadResource( itemName, count );
		if ( unloaded > 0 )
		{
			wallet.Balance += unloaded * SaleOffers[itemName];
		}
	}

	protected override void OnUpdate()
	{

	}
}
