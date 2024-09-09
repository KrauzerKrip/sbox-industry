using Sandbox;
using Sandbox.Exceptions;

public sealed class Inventory : Component
{
	[Property, Category( "Components" )]
	public GameObject ResourceCrateObject { get; set; }

	[Property]
	[Range(1, 999)]
	[Category("Stats")]
	public int MaxSlots { get; set; }

	[Property]
	[Range( 1, 999999 )]
	[Category( "Stats" )]
	public float MaxMass { get; set; }

	public Dictionary<string, float> Resources { get; private set; } = new Dictionary<string, float>();


	public float TryLoadResource(string name, float mass)
	{
		if ( Resources.Count >= MaxSlots)
		{
			return 0;
		}

		float inventoryMass = GetInventoryMass();
		float canLoad = MaxMass - inventoryMass;

		if (canLoad < 0)
		{
			throw new InventoryException( "Inventory mass > MaxMass" );
		}

		float loaded;

		if (mass < canLoad)
		{
			loaded = mass;
		} else
		{
			loaded = canLoad;
		}

		if (Resources.ContainsKey(name))
		{
			Resources[name] += loaded;
		} else
		{
			Resources[name] = loaded;
		}

		return loaded;
	}

	public float TryUnloadResource(string resourceName, float mass)
	{
		if ( Resources.TryGetValue( resourceName, out float resourceMass))
		{
			if (mass > resourceMass)
			{
				Resources.Remove( resourceName );
				return resourceMass;
			} else
			{
				Resources[resourceName] -= mass;
				return mass;
			}
		} else
		{
			return 0;
		}
	}

	public void ForceLoadResource(string resourceName, float mass)
	{
		float loaded = TryLoadResource( resourceName, mass );
		float rest = mass - loaded;

		if ( rest > 0 )
		{
			GameObject resourceCrateObject = ResourceCrateObject.Clone();
			resourceCrateObject.Transform.Position = GameObject.Transform.Position;
			ResourceCrate resourceCrate = resourceCrateObject.Components.Get<ResourceCrate>();
			if ( resourceCrate == null )
			{
				throw new ComponentException( "ResourceCrate object doesn't have ResourceCrate component" );
			}
			resourceCrate.ResourceName = resourceName;
			resourceCrate.ResourceMass = rest;
		}
	}

	protected override void OnUpdate()
	{

	}


	private float GetInventoryMass()
	{
		float inventoryMass = 0;
		foreach (var (res, mass) in Resources)
		{
			inventoryMass += mass;
		}

		return inventoryMass;
	}
}
