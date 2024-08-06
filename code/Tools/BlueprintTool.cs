using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Tools
{
	public sealed class BlueprintTool : ToolBase
	{
		[Property]
		public Dictionary<string, GameObject> Blueprints { get; set; }
		[Property]
		PlayerController PlayerController { get; set; }

		private GameObject _currentBlueprint;

		public BlueprintTool()
		{
			Name = "blueprint_tool";
		}

		protected override void OnUpdate()
		{
			if (_currentBlueprint != null)
			{
				SceneTraceResult result = Scene.Trace.Ray(PlayerController.AimRay, 1000 ).WithAnyTags( "terrain" ).Run();
				result.EndPosition.x = (result.EndPosition.x / 10).Floor() * 10;
				result.EndPosition.y = (result.EndPosition.y / 10).Floor() * 10;
				result.EndPosition.z = (result.EndPosition.z / 10).Floor() * 10;
				_currentBlueprint.Transform.Position = result.EndPosition;
			}

			base.OnUpdate();
		}

		public override void Attack1()
		{
			Blueprint blueprintComponent = _currentBlueprint.Components.GetInChildrenOrSelf<Blueprint>();

			if (blueprintComponent == null)
			{
				throw new Exception( "Blueprint Tool: current blueprint object doesn't have Blueprint component." );
			}

			if ( blueprintComponent.Placeable ) {
				blueprintComponent.Place();
				_currentBlueprint = null;
			}
		}

		public override void Attack2()
		{
			
		}

		public override void Reload()
		{
			
		}

		public void SpawnBlueprint(string blueprintName)
		{
			if (Blueprints.TryGetValue(blueprintName, out var blueprint))
			{
				_currentBlueprint = blueprint.Clone();
			}
		}
	}
}
