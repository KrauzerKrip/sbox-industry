using Sandbox;

public sealed class Wallet : Component
{
	[Property, Category("Info"), Range(0, 999999999999)]
	public double Balance { get; set; }
	protected override void OnUpdate()
	{

	}
}
