using NativeEngine;
using Sandbox;
using Sandbox.Exceptions;
using Sandbox.Machines;
using System;

enum BlueprintMaterial
{
	DEFAULT, 
	DANGER
}

public sealed class Blueprint : Component
{
	[Property, Category( "Components" )]
	public ModelRenderer Renderer { get; set; }
	[Property, Category( "Components" )]
	public ModelPhysics Physics { get; set; }
	[Property, Category( "Components" )]
	public ModelCollider Collider { get; set; }
	[Property, Category( "Components" )]
	public HighlightOutline Outline { get; set; }
	[Property, Category( "Components" )]
	public Recipe Recipe { get; set; }
	[Property, Category( "Components" )]
	public Inventory Inventory { get; set; }

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

		Tags.Add( "blueprint" );

		base.OnStart();
	}

	protected override void OnFixedUpdate()
	{
		Placeable = true;
		Buildable = true;

		DisableOutline();

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

	public void EnableOutline()
	{
		Outline.Enabled = true;
	}

	public void DisableOutline()
	{
		Outline.Enabled = false;
	}

	public void Use( GameObject user )
	{
		Inventory userInventory = user.Components.Get<Inventory>();	
		if (userInventory == null)
		{
			throw new ComponentException( "Player " + user.Name + " doesn't have Inventory component." );
		}

		foreach ( var (reqRes, reqMass) in Recipe.Ingredients)
		{
			if (Inventory.Resources.TryGetValue(reqRes, out float mass)) {
				float howMuchDoWeNeedStill = reqMass - mass;
				if (howMuchDoWeNeedStill < 0) {
					throw new Exception( "Beri proraba" );
				}

				float unloadedMass = userInventory.TryUnloadResource( reqRes, howMuchDoWeNeedStill );
				Inventory.Resources[reqRes] += unloadedMass;
			} else
			{
				Inventory.Resources[reqRes] = 0;
			}
		}

		if (Recipe.IsInventorySufficient( Inventory ))
		{
			Build();
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

	public void Build()
	{
		if ( Buildable ) {
			Enabled = false;
			Tags.Remove( "blueprint" );
			Tags.Add( "machine" );
			DisableOutline();
			Physics.Enabled = true;
			Collider.IsTrigger = false;
			Renderer.ClearMaterialOverrides();
		}
	}
}
