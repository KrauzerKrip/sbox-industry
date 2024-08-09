using Sandbox;

public sealed class ResourceCrate : Component
{
	[Property]
	[Range(0, 100)]
	public float Mass { get; set; }
	[Property]
	public ResourceBase Resource { get; set; }

	private const float _removalDelta = 0.0001f;

	protected override void OnFixedUpdate()
	{
		if (Mass <= _removalDelta)
		{
			GameObject.Destroy();
		}
	}
}
