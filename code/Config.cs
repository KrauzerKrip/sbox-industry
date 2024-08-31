using Sandbox;

public sealed class Config : Component
{
	protected override void OnStart()
	{
		ConsoleSystem.SetValue( "game_blueprint_required", true );

		base.OnStart();
	}

	protected override void OnUpdate()
	{

	}
}
