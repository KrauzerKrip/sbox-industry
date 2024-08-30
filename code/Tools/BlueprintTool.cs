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
		[Property]
		public GameObject ConnectionObject { get; set; }
		[Property]
		public AcquiredBlueprints AcquiredBlueprints { get; set; }

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
				MachineBase machineBase = _currentBlueprint.Components.GetInChildrenOrSelf<MachineBase>();
				if ( machineBase == null )
				{
					throw new ComponentException( "Current blueprint tool blueprint doesn't have MachineBase component." );
				}

				SceneTraceResult resultTerrain = Scene.Trace.Ray( PlayerController.AimRay, 1000 ).WithTag( "terrain" ).Run();
				resultTerrain.EndPosition.x = (resultTerrain.EndPosition.x / 10).Floor() * 10;
				resultTerrain.EndPosition.y = (resultTerrain.EndPosition.y / 10).Floor() * 10;
				resultTerrain.EndPosition.z = (resultTerrain.EndPosition.z / 10).Floor() * 10;
				_currentBlueprint.Transform.Position = resultTerrain.EndPosition;

				if ( machineBase.IsAttachment )
				{
					SceneTraceResult result = Scene.Trace.Ray( PlayerController.AimRay, 1000 ).HitTriggers().WithAnyTags( "machine", "blueprint" ).Run();
					if ( result.GameObject != null && result.GameObject.IsValid() )
					{
						MachineBase otherMachineBase = result.GameObject.Components.GetInChildrenOrSelf<MachineBase>();
						if (otherMachineBase == null)
						{
							throw new ComponentException( "A GameObject with the tag 'machine' or 'blueprint' doesn't have MachineBase component." );
						}

						foreach ( ResourceConnector connector in machineBase.Connectors )
						{
							foreach ( ResourceConnector otherConnector in otherMachineBase.Connectors )
							{
								int connectionDirection = 0;
								if ( connector.ConnectionType == ConnectionType.In && otherConnector.ConnectionType == ConnectionType.Out )
								{
									connectionDirection = 1;
								}
								if ( connector.ConnectionType == ConnectionType.Out && otherConnector.ConnectionType == ConnectionType.In )
								{
									connectionDirection = 2;
								}

								if ( connector.ResourceConnection != null )
								{
									if ( otherConnector.ResourceConnection == connector.ResourceConnection )
									{
										continue;
									}
								}

								if ( connector.AllowedResourceTypes.SequenceEqual( otherConnector.AllowedResourceTypes ) && connectionDirection != 0 )
								{
									GameObject connectionObject = ConnectionObject.Clone();
									ResourceConnection connection = connectionObject.Components.GetOrCreate<ResourceConnection>();

									if ( connectionDirection == 1 )
									{
										connection.ResourceConnectorIn = connector;
										connection.ResourceConnectorOut = otherConnector;
									}
									else if ( connectionDirection == 2 )
									{
										connection.ResourceConnectorOut = connector;
										connection.ResourceConnectorIn = otherConnector;
									}

									if (connector.ResourceConnection != null)
									{
										connector.ResourceConnection.GameObject.Destroy();
									}

									connector.ResourceConnection = connection;
									otherConnector.ResourceConnection = connection;

									otherConnector.GameObject.AddSibling( _currentBlueprint , false);
								}
							}
						}

						foreach ( ResourceConnector connector in machineBase.Connectors )
						{
							if ( connector.ResourceConnection != null )
							{
								GameTransform transform = null;
								if ( connector.ConnectionType == ConnectionType.In)
								{
									transform = connector.ResourceConnection.ResourceConnectorOut.GameObject.Transform;
								} else
								{
									transform = connector.ResourceConnection.ResourceConnectorIn.GameObject.Transform;
								}
								_currentBlueprint.Transform.Position = transform.World.Position;
							}
						}
					}
				}
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
					throw new ComponentException( "A GameObject with the tag 'blueprint' doesn't have Blueprint component." );
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
					MachineBase machineBase = _blueprintLookingAt.Components.Get<MachineBase>();
					if (machineBase == null)
					{
						throw new ComponentException( "Blueprint Tool: current blueprint object doesn't have MachineBase component." );
					}

					foreach (ResourceConnector resourceConnector in machineBase.Connectors ) {
						if ( resourceConnector.ResourceConnection.GameObject != null)
						{
							resourceConnector.ResourceConnection.GameObject.Destroy();
						}
					}

					_blueprintLookingAt.Destroy();
				}
			} else
			{
				Blueprint blueprintComponent = _currentBlueprint.Components.GetInChildrenOrSelf<Blueprint>();

				if ( blueprintComponent == null )
				{
					throw new ComponentException( "Blueprint Tool: current blueprint object doesn't have Blueprint component." );
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
			if ( _currentBlueprint == null )
			{
				return;
			}

			Angles angles = _currentBlueprint.Transform.Rotation.Angles();
			angles.yaw += 90f;

			_currentBlueprint.Transform.Rotation = angles.ToRotation();
		}

		public void SpawnBlueprint(string blueprintName)
		{
			if ( !AcquiredBlueprints.Blueprints.Exists( x => x.Equals( blueprintName ) ) )
			{
				return;
			}

			if ( Blueprints.TryGetValue( blueprintName, out var blueprint ) )
			{
				_currentBlueprint = blueprint.Clone();
			}
		}
	}
}
