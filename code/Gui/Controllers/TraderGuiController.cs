using Sandbox;
using Sandbox.Economy;
using Sandbox.Exceptions;
using Sandbox.Machines;

public sealed class TraderGuiController : TraderSubscriber
{
	public bool IsShown { get; private set; } = false;
	
	[Property]
	public TraderGui TraderGui { get; set; }

	protected override void OnStart()
	{
		TraderGui.OnPurchase += ( string name, float count ) =>
		{
			Log.Info( $"Purchased {count} of {name} " );
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

		IsShown = true;
		TraderGui.IsShown = true;
		playerController.IsFreezed = true;
		playerController.IsGuiOpened = true;

		playerController.OnHideGui += () => { HideTraderGui( user, trader ); };
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
	}

	protected override void OnUpdate()
	{
		TraderGui.PurchaseOffers = new Dictionary<string, float> {
			{ "wood", 5 },
			{ "cat", 10 }
		};
	}
}
