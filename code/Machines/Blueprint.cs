using NativeEngine;
using Sandbox;

public sealed class Blueprint : Component
{
	[Property]
	[Category("Components")]
	public ModelRenderer Renderer { get; set; }
	[Property]
	[Category("Components")]
	public ModelPhysics Physics { get; set; }
	public GameObject Owner { get; set; }
	public string Name { get; set; }

	protected override void OnStart()
	{
		Material material = Material.Load( "materials\\blueprint.vmat" );

		if (material == null)
		{
			Log.Error( "NULL" );
		}
		if (material.IsPromise)
		{
			Log.Error( "PROMISE" );
		}

		Renderer.SetMaterial( material );

		Physics.Enabled = false;

		base.OnStart();
	}

	protected override void OnUpdate()
	{

	}
}
