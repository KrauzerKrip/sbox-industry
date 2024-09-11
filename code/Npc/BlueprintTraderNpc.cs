	using Sandbox;
using Sandbox.Npc;

using Sandbox.Npc.Dialogues;
using Sandbox.Player;
using Sandbox.Exceptions;
using System;

enum Stage
{
	Introduction
}

public sealed class BlueprintTraderNpc : Npc, IUsable
{
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

		Dialogue requiredDialogue = GetRequiredDialogue( _playerStages[playerInfo.Guid] );
		if ( requiredDialogue != null ) {
			StartDialogue( user, requiredDialogue );
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
