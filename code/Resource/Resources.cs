using Sandbox;

public sealed class Resources : Component
{
	[Property]
	public Dictionary<string, GameObject> Objects { get; set; } = new Dictionary<string, GameObject>();

	public GameObject GetByName( string name )
	{
		return Objects.GetValueOrDefault( name );
	}

}
