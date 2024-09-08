using Sandbox;
using Sandbox.Citizen;
using System.Numerics;

public sealed partial class PlayerController : Component
{
	[Property]
	[Category( "Components" )]
	public GameObject Camera { get; set; }
	[Property]
	[Category( "Components" )]
	public CharacterController CharacterController { get; set; }
	[Property]
	[Category( "Components" )]
	public CitizenAnimationHelper CitizenAnimationHelper { get; set; }
	[Property]
	[Category( "Stats" )]
	[Range( 0f, 400f, 1f )]
	public float WalkSpeed { get; set; } = 200f;
	[Property]
	[Category( "Stats" )]
	[Range( 0f, 800f, 1f )]
	public float RunSpeed { get; set; } = 300f;
	[Property]
	[Category( "Stats" )]
	[Range( 0f, 1000f, 1f )]
	public float JumpStrength { get; set; } = 400f;
	[Property]
	[Category( "Controls" )]
	public bool IsMachineMenuOpened { get; set; } = false;
	[Property]
	[Category( "Controls" )]
	public bool IsGuiOpened { get; set; } = false;
	[Property]
	[Category( "Controls" )]
	public bool IsFreezed { get; set; } = false;

	public Angles EyeAngles { get; set; }

	public Ray AimRay
	{
		get
		{
			return new( Camera.Transform.Position + Camera.Transform.Rotation.Forward, Camera.Transform.Rotation.Forward );
		}
	}

	protected override void OnUpdate()
	{
		if ( !IsFreezed )
		{
			EyeAngles += Input.AnalogLook;
			Transform.Rotation = Rotation.FromYaw( EyeAngles.yaw );
			Camera.Transform.Rotation = Rotation.FromYaw( EyeAngles.yaw ) * Rotation.FromPitch( EyeAngles.pitch.Clamp( -90f, 90f ) );
		}
	}

	protected override void OnFixedUpdate()
	{

		float moveSpeed;

		if ( Input.Down( "Run" ) )
		{
			moveSpeed = RunSpeed;
		}
		else
		{
			moveSpeed = WalkSpeed;
		}

		if (Input.Pressed( "Menu" ))
		{
			IsMachineMenuOpened = !IsMachineMenuOpened;
			IsInventoryOpened = false;
			if ( OnHideGui != null )
			{
				OnHideGui();
			}
		}

		if (Input.Pressed( "attack1" ) )
		{;
			GetCurrentTool().Attack1();
		}
		if ( Input.Pressed( "attack2" ) )
		{
			GetCurrentTool().Attack2();
		}
		if ( Input.Pressed( "reload" ) )
		{
			GetCurrentTool().Reload();
		}

		if ( Input.Pressed( "Inventory" ) & !IsGuiOpened )
		{
			IsInventoryOpened = !IsInventoryOpened;
			IsMachineMenuOpened = false;
		}
		
		if ( Input.Pressed( "use" ) )
		{
			bool wasGuiOpened = false;
			if ( IsGuiOpened )
			{
				wasGuiOpened = true;
				if ( OnHideGui != null )
				{
					OnHideGui();
				}
			}

			if ( !wasGuiOpened && !IsGuiOpened)
			{
				TryUse();
			}

			TryUseBlueprint();
		}

		if (Input.Pressed( "Build") )
		{
			if (GetCurrentTool().Modes.TryGetValue( "Build", out System.Action action ) )
			{
				action();
			}
		}
		if ( Input.Pressed( "Remove" ) )
		{
			if ( GetCurrentTool().Modes.TryGetValue( "Remove", out System.Action action ) )
			{
				action();
			}
		}

		if ( Input.Pressed( "Slot0" ) )
		{
			SetToolIndex( 0 );
		}
		if ( Input.Pressed( "Slot1" ) )
		{ 
			SetToolIndex( 1 );
		}
		if ( Input.Pressed( "Slot2" ) )
		{
			SetToolIndex( 2 );
		}


		if ( IsFreezed )
		{
			if ( CharacterController.IsOnGround )
			{
				CharacterController.ApplyFriction( 5f );
			}
			else
			{
				CharacterController.Velocity += Scene.PhysicsWorld.Gravity * Time.Delta;
			}
		} else
		{

			var wishVelocity = Input.AnalogMove * moveSpeed * Transform.Rotation;

			CharacterController.Accelerate( wishVelocity );
			if ( CharacterController.IsOnGround )
			{
				CharacterController.ApplyFriction( 5f );

				if ( Input.Pressed( "Jump" ) )
				{
					CharacterController.Punch( Vector3.Up * JumpStrength );

					CitizenAnimationHelper.TriggerJump();
				}
			}
			else
			{
				CharacterController.Velocity += Scene.PhysicsWorld.Gravity * Time.Delta;
			}
		}

		CharacterController.Move();

		CitizenAnimationHelper.IsGrounded = CharacterController.IsOnGround;
		CitizenAnimationHelper.WithVelocity( CharacterController.Velocity );

		base.OnFixedUpdate();
	}

	protected override void OnStart()
	{
		if (Components.TryGet<SkinnedModelRenderer>(out var model))
		{
			var clothing = ClothingContainer.CreateFromLocalUser();
			clothing.Apply( model );
		}

		base.OnStart();
	}

	protected override void OnEnabled()
	{
		base.OnEnabled();
	}

	protected override void OnDisabled()
	{
		base.OnDisabled();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}


}
