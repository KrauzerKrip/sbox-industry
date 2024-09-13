using Sandbox;
using Sandbox.Gui.Controllers;
using Sandbox.Npc;

public sealed class DialogueGuiController : Component
{
	[Property, Category("Components")]
	public DialogueGui DialogueGui { get; set; }
	[Property, Category( "Components" )]
	public DialogueManager DialogueManager { get; set; }
	[Property, Category( "Components" )]
	public PlayerController PlayerController { get; set; }

	private DialogueHandler _currentDialogueHandler;



	protected override void OnStart()
	{
		DialogueManager.OnDialogueStart += ( GameObject player, Dialogue dialogue ) =>
		{
			_currentDialogueHandler = new DialogueHandler( dialogue );
			_currentDialogueHandler.Start();

			DialogueNode node = _currentDialogueHandler.GetCurrentNode();
			DialogueGui.CharacterName = node.CharacterName;
			DialogueGui.Text = node.Text;
			DialogueGui.CharacterAvatarImage = node.AvatarImagePath;
			node.Activate( PlayerController.GameObject );
			ShowDialogueGui();
		};

		DialogueGui.OnDialogueSkip += () =>
		{
			_currentDialogueHandler.Next();

			DialogueNode node = _currentDialogueHandler.GetCurrentNode();
			DialogueGui.CharacterName = node.CharacterName;
			DialogueGui.Text = node.Text;
			DialogueGui.CharacterAvatarImage = node.AvatarImagePath;
		};

		base.OnStart();
	}

	public void ShowDialogueGui()
	{
		DialogueGui.IsShown = true;
	}

	public void HideDialogueGui()
	{
		DialogueGui.IsShown = false;
	}
}
