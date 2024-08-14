using Sandbox;

public enum ConnectionType
{
	In,
	Out
}

namespace Sandbox.Machines
{
	public sealed class ResourceConnector : Component
	{
		[Property, Category( "Info" )]
		public ConnectionType ConnectionType { get; set; }
		[Property, Category( "Info" )]
		public List<string> AllowedResourceTypes { get; set; }
		[Property, Category( "Info" )]
		public float ResourceAmount { get; set; }
		[Property, Category( "Info") ]
		public string CurrentResource { get; set; }
		[Property]
		public ResourceConnection ResourceConnection { get; set; }


		protected override void OnUpdate()
		{


		}
	}
}
