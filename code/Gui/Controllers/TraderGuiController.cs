using Sandbox;
using Sandbox.Economy;
using Sandbox.Exceptions;
using Sandbox.Machines;


public enum Tabs
{
	Purchase,
	Sale
}
public sealed class TraderGuiController : TraderSubscriber
{
	public bool IsShown { get; private set; } = false;
	
	[Property]
	public TraderGui TraderGui { get; set; }
	[Property]
	public TraderBase CurrentTrader { get; set; }
	[Property]
	public PlayerController PlayerController { get; set; }

	protected override void OnStart()
	{
		TraderGui.OnPurchase += ( string name, float count ) =>
		{
			CurrentTrader.Buy( PlayerController.GameObject, name, count );
		};
		TraderGui.OnSale += ( string name, float count ) =>
		{
			CurrentTrader.Sell( PlayerController.GameObject, name, count );
		};

		base.OnStart();
	}

	public override void SubscribeOnTrader( GameObject traderObject )
	{
		TraderBase trader = traderObject.Components.Get<TraderBase>();
		if ( trader == null )
		{
			throw new ComponentException( "Trader " + traderObject.Name + " doesn't have TraderBase component." );
		}
		trader.OnUse += ( GameObject user, GameObject trader ) => {
			if ( !IsShown )
			{
				ShowTraderGui( user, trader );
			}
		};
	}

	private void ShowTraderGui( GameObject user, GameObject trader )
	{
		PlayerController playerController = user.Components.Get<PlayerController>();
		if ( playerController == null )
		{
			throw new ComponentException( "User " + user.Name + " doesn't have PlayerController component." );
		}
		TraderBase traderBase = trader.Components.Get<TraderBase>();
		if ( traderBase == null )
		{
			throw new ComponentException( "Trader " + trader.Name + " doesn't have TraderBase component." );
		}

		IsShown = true;
		TraderGui.IsShown = true;
		TraderGui.Tab = Tabs.Purchase;
		playerController.IsFreezed = true;
		playerController.IsGuiOpened = true;

		playerController.OnHideGui += () => { HideTraderGui( user, trader ); };

		CurrentTrader = traderBase;
	}

	private void HideTraderGui( GameObject user, GameObject trader )
	{
		PlayerController playerController = user.Components.Get<PlayerController>();
		if ( playerController == null )
		{
			throw new ComponentException( "User " + user.Name + " doesn't have PlayerController component." );
		}

		IsShown = false;
		TraderGui.IsShown = false;
		playerController.IsFreezed = false;
		playerController.IsGuiOpened = false;

		CurrentTrader = null;
	}

	protected override void OnUpdate()
	{
		TraderGui.PurchaseOffers = new Dictionary<string, float> {
			{ "wood", 5 },
			{ "cat", 10 }
		};
		TraderGui.SaleOffers = new Dictionary<string, float> {
			{ "dog", 1 }
		};

		if ( CurrentTrader != null )
		{
			TraderGui.PurchaseOffers = CurrentTrader.PurchaseOffers;
			TraderGui.SaleOffers = CurrentTrader.SaleOffers;
		}
	}
}
