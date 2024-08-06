using NativeEngine;
using Sandbox;

enum BlueprintMaterial
{
	DEFAULT, 
	DANGER
}

public sealed class Blueprint : Component
{
	[Property]
	[Category("Components")]
	public ModelRenderer Renderer { get; set; }
	[Property]
	[Category("Components")]
	public ModelPhysics Physics { get; set; }
	[Property]
	[Category("Components")]
	public ModelCollider Collider { get; set; }
	public GameObject Owner { get; set; }
	public string Name { get; set; }
	public bool Buildable { get; private set; } = false;
	public bool Placeable { get; private set; } = false;
	private Material _defaultMaterial;
	private Material _dangerMaterial;
	private BlueprintMaterial _currentMaterial;

	protected override void OnStart()
	{
		_defaultMaterial = Material.Load( "materials\\blueprint.vmat" );
		_dangerMaterial = Material.Load( "materials\\blueprint_danger.vmat" );

		Renderer.SetMaterial( _defaultMaterial );
		_currentMaterial = BlueprintMaterial.DEFAULT;

		Physics.Enabled = true;
		Collider.IsTrigger = true;

		base.OnStart();
	}

	protected override void OnFixedUpdate()
	{
		Placeable = true;
		Buildable = true;

		foreach ( Collider col in Collider.Touching )
		{
			if ( col.GameObject.Tags.HasAny( "machine", "player" ) )
			{
				Placeable = false;
				Buildable = false;
			}
		}

		if ( Placeable && _currentMaterial == BlueprintMaterial.DANGER )
		{
			Renderer.SetMaterial( _defaultMaterial );
			_currentMaterial = BlueprintMaterial.DEFAULT;
		}
		else if ( !Placeable && _currentMaterial == BlueprintMaterial.DEFAULT )
		{
			Renderer.SetMaterial( _dangerMaterial );
			_currentMaterial = BlueprintMaterial.DANGER;
		}

		base.OnFixedUpdate();
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
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
