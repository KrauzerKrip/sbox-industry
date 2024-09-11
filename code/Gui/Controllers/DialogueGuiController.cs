using Sandbox;
using Sandbox.Npc;

public sealed class DialogueGuiController : Component
{
	[Property, Category("Components")]
	public DialogueGui DialogueGui { get; set; }
	[Property, Category( "Components" )]
	public DialogueManager DialogueManager { get; set; }

	protected override void OnStart()
	{
		DialogueManager.OnDialogueStart += ( GameObject player, Dialogue dialogue ) =>
		{
			DialogueGui.CharacterName = dialogue.Tree.Root.CharacterName;
			DialogueGui.Text = dialogue.Tree.Root.Text;
			DialogueGui.CharacterAvatarImage = dialogue.Tree.Root.AvatarImagePath;
			dialogue.Tree.Root.Activate( player );
			ShowDialogueGui();
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
