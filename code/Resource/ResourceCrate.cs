using Sandbox;
using Sandbox.Exceptions;
using Sandbox.Player;

public sealed class ResourceCrate : Component, IUsable
{
	[Property]
	[Range(0, 100)]
	public float ResourceMass { get; set; }
	[Property]
	public string ResourceName { get; set; }

	private const float _removalDelta = 0.0001f;

	public void Use( GameObject user )
	{
		Inventory inventory = user.Components.Get<Inventory>();
		if ( inventory == null )
		{
			throw new ComponentException( "User doesn't have Inventory component" );
		}

		float loadedMass = inventory.TryLoadResource( ResourceName, ResourceMass );
		ResourceMass -= loadedMass;
		Log.Info( "Loaded " + loadedMass + " of " + ResourceName );
	} 

	protected override void OnFixedUpdate()
	{
		if (ResourceMass <= _removalDelta)
		{
			GameObject.Destroy();
		}
	}
}
