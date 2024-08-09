using Sandbox;
using Sandbox.Exceptions;

public sealed class Inventory : Component
{
	[Property]
	[Range(1, 999)]
	[Category("Stats")]
	public int MaxSlots { get; set; }

	[Property]
	[Range( 1, 999999 )]
	[Category( "Stats" )]
	public float MaxMass { get; set; }

	public Dictionary<string, float> Resources { get; private set; } = new Dictionary<string, float>();

	public float TryLoadResource(ResourceBase resource, float mass)
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

		if (Resources.ContainsKey(resource.Name))
		{
			Resources[resource.Name] += loaded;
		} else
		{
			Resources[resource.Name] = loaded;
		}

		return loaded;
	}

	public float TryUnloadResource(ResourceBase resource, float mass)
	{
		if ( Resources.TryGetValue(resource.Name, out float resourceMass))
		{
			if (mass > resourceMass)
			{
				Resources.Remove(resource.Name);
				return resourceMass;
			} else
			{
				Resources[resource.Name] -= mass;
				return mass;
			}
		} else
		{
			return 0;
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
