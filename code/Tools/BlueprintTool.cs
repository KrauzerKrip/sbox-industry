using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Tools
{
	enum Mode
	{
		Builder,
		Remover
	}

	public sealed class BlueprintTool : ToolBase
	{
		[Property]
		public Dictionary<string, GameObject> Blueprints { get; set; }
		[Property]
		PlayerController PlayerController { get; set; }

		private GameObject _currentBlueprint;
		private GameObject _blueprintLookingAt;
		private Mode _mode;

		public BlueprintTool()
		{
			Name = "blueprint_tool";
			_mode = Mode.Builder;
			CurrentMode = "Builder";
			Modes.Add( "Remove", () => {
				_mode = Mode.Remover;
				CurrentMode = "Remover";
				if ( _currentBlueprint != null ) {
					_currentBlueprint.Destroy();
					_currentBlueprint = null; };
			}
			);
			Modes.Add( "Build", () => {
				_mode = Mode.Builder;
				CurrentMode = "Builder";
			} ) ;
		}

		protected override void OnUpdate()
		{
			if ( _currentBlueprint != null )
			{
				SceneTraceResult result = Scene.Trace.Ray( PlayerController.AimRay, 1000 ).WithTag( "terrain" ).Run();
				result.EndPosition.x = (result.EndPosition.x / 10).Floor() * 10;
				result.EndPosition.y = (result.EndPosition.y / 10).Floor() * 10;
				result.EndPosition.z = (result.EndPosition.z / 10).Floor() * 10;
				_currentBlueprint.Transform.Position = result.EndPosition;
			}

			SceneTraceResult resultBlueprint = Scene.Trace.Ray( PlayerController.AimRay, 1000 ).HitTriggers().WithTag( "blueprint" ).Run();

			_blueprintLookingAt = null;

			if ( resultBlueprint.GameObject != null && resultBlueprint.GameObject.IsValid() )
			{

				if ( resultBlueprint.GameObject.Components.TryGet( out Blueprint blueprint ) )
				{
					_blueprintLookingAt = resultBlueprint.GameObject;
					blueprint.EnableOutline();
				}
				else
				{
					throw new Exception( "A GameObject with the tag 'blueprint' doesn't have Blueprint component." );
				}
			}

			base.OnUpdate();
		}

		public override void Equip()
		{
			_mode = Mode.Builder;
			CurrentMode = "Builder";
		}
		public override void Attack1()
		{
			if (_currentBlueprint == null)
			{
				if (_mode == Mode.Remover && _blueprintLookingAt != null)
				{
					_blueprintLookingAt.Destroy();
				}
			} else
			{
				Blueprint blueprintComponent = _currentBlueprint.Components.GetInChildrenOrSelf<Blueprint>();

				if ( blueprintComponent == null )
				{
					throw new Exception( "Blueprint Tool: current blueprint object doesn't have Blueprint component." );
				}

				if ( blueprintComponent.Placeable )
				{
					blueprintComponent.Place();
					_currentBlueprint = null;
				}
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
