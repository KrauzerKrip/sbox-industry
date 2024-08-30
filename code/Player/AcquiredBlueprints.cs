using Sandbox;

public sealed class AcquiredBlueprints : Component
{
	[Property]
	public List<string> Blueprints { get; set; } = new List<string>();
}
