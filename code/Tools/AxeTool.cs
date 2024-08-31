using Sandbox.Agriculture;
using Sandbox.Exceptions;
using Sandbox.Machines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sandbox.Tools
{
	public sealed class AxeTool : ToolBase
	{

		[Property]
		PlayerController PlayerController { get; set; }

		public AxeTool()
		{
			Name = "axe_tool";
		}

		protected override void OnStart()
		{
			ConsoleSystem.SetValue( "game_blueprint_required", true );

			base.OnStart();
		}

		protected override void OnUpdate()
		{
		}

		public override void Equip()
		{
		}

		public override void Attack1()
		{
			SceneTraceResult result = Scene.Trace.Ray( PlayerController.AimRay, 100 ).WithTag( "harvestable" ).Run();
			if ( result.GameObject != null && result.GameObject.IsValid() )
			{
				IHarvestable harvestable = result.GameObject.Components.Get<IHarvestable>();
				if ( harvestable == null )
				{
					throw new ComponentException( "Object with tag 'harvestable' doesn't have a component implementing IHarvestable" );
				}

				harvestable.Harvest( PlayerController.GameObject );
			}
		}

		public override void Attack2()
		{
			
		}

		public override void Reload()
		{
	
		}
	}
}
