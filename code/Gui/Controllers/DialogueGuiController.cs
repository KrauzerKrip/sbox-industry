using Sandbox;

public sealed class DialogueGuiController : Component
{
	[Property, Category("Components")]
	public DialogueGui DialogueGui { get; set; }

	public void ShowDialogueGui()
	{
		DialogueGui.IsShown = true;
	}

	public void HideDialogueGui()
	{
		DialogueGui.IsShown = false;
	}
}
