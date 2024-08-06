using NativeEngine;
using Sandbox;

public sealed class Blueprint : Component, Component.ICollisionListener
{
	[Property]
	[Category("Components")]
	public ModelRenderer Renderer { get; set; }
	[Property]
	[Category("Components")]
	public ModelPhysics Physics { get; set; }
	public GameObject Owner { get; set; }
	public string Name { get; set; }
	public bool Buildable { get; private set; } = false;
	public bool Placeable { get; private set; } = false;
	private Material _defaultMaterial;
	private Material _dangerMaterial;

	protected override void OnStart()
	{
		_defaultMaterial = Material.Load( "materials\\blueprint.vmat" );
		_dangerMaterial = Material.Load( "materials\\blueprint_danger.vmat" );

		Renderer.SetMaterial( _defaultMaterial );

		Physics.Enabled = true;

		base.OnStart();
	}

	public void OnCollisionUpdate( Collision collision )
	{
		Log.Info( "TOUCH" );

		Placeable = true;

		GameObject otherObject = collision.Other.GameObject;

		if (otherObject.Tags.Has("machine") || otherObject.Tags.Has("player")) {
			Placeable = false;
		}
	}

	protected override void OnUpdate()
	{
		if (Placeable && Renderer.MaterialOverride.Name == "materials\\blueprint_danger.vmat" )
		{
			Renderer.SetMaterial( _defaultMaterial );
		}
		else if (!Placeable && Renderer.MaterialOverride.Name == "materials\\blueprint.vmat" )
		{
			Renderer.SetMaterial( _dangerMaterial );
		}
	}

	public void Place()
	{
		if (!Placeable)
		{
			throw new System.Exception( "Blueprint: can't be placed." );
		}

		Buildable = true;
	}
}
