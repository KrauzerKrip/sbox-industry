	using Sandbox;
using Sandbox.Npc;

using Sandbox.Npc.Dialogues;
using Sandbox.Player;
using Sandbox.Exceptions;
using System;

enum Stage
{
	Introduction,
	Trade
}

public sealed class BlueprintTraderNpc : Npc, IUsable
{
	[Property]
	public BlueprintTrader BlueprintTrader { get; set; }

	private readonly Dialogue _introDialogue;
	private Dictionary<Guid, Stage> _playerStages = new(); 

	public BlueprintTraderNpc()
	{
		_introDialogue = new BTDialogue1();
	}

	public void Use( GameObject user )
	{
		PlayerInfo playerInfo = user.Components.Get<PlayerInfo>();
		if ( playerInfo == null )
		{
			throw new ComponentException($"User {user.Name} doesn't have PlayerInfo component.");
		}

		if ( !_playerStages.ContainsKey( playerInfo.Guid ) )
		{
			_playerStages[playerInfo.Guid] = Stage.Introduction;
		}

		if ( _playerStages[playerInfo.Guid] == Stage.Trade )
		{
			BlueprintTrader.Use( user );
		}

		Dialogue requiredDialogue = GetRequiredDialogue( _playerStages[playerInfo.Guid] );
		if ( requiredDialogue != null ) {
			StartDialogue( user, requiredDialogue );
			_playerStages[playerInfo.Guid] = Stage.Trade;
		}
	}

	protected override void OnUpdate()
	{
		
	}

	private Dialogue GetRequiredDialogue(Stage stage)
	{
		if (stage == Stage.Introduction)
		{
			return _introDialogue;
		}

		return null;
	} 
}
