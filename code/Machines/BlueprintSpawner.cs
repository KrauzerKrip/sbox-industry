using Sandbox;

public sealed class BlueprintSpawner : Component
{
	[Property]
	public Dictionary<string, GameObject> Blueprints { get; set; }

	public void SpawnOnRay(string machineName, Ray ray)
	{
		SceneTraceResult result = Scene.Trace.Ray( ray, 1000 ).WithAnyTags( "terrain" ).Run();

		if (Blueprints.TryGetValue(machineName, out var blueprint)) {
			blueprint.Clone( result.EndPosition );
		}
	}
}
