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
	private string _currentText;
	private int _printedTextLength;
	private bool _isTextPrintFinished;
	private float _timeElapsed = 0;
	private float _deltaTime = 0.03f;

	protected override void OnStart()
	{
		DialogueManager.OnDialogueStart += ( GameObject player, Dialogue dialogue ) =>
		{
			_currentDialogueHandler = new DialogueHandler( dialogue );
			_currentDialogueHandler.Start();

			DialogueNode node = _currentDialogueHandler.GetCurrentNode();
			DialogueGui.CharacterName = node.CharacterName;
			DialogueGui.Text = "";
			_currentText = node.Text;
			_printedTextLength = 0;
			_isTextPrintFinished = false;
			DialogueGui.CharacterAvatarImage = node.AvatarImagePath;
			node.Activate( PlayerController.GameObject );
			ShowDialogueGui();
		};

		DialogueGui.OnDialogueSkip += () =>
		{
			if ( _currentDialogueHandler.IsNodeLast() && _isTextPrintFinished )
			{
				HideDialogueGui();
			}

			if ( _isTextPrintFinished )
			{
				_currentDialogueHandler.Next();
				DialogueNode node = _currentDialogueHandler.GetCurrentNode();
				DialogueGui.CharacterName = node.CharacterName;
				DialogueGui.CharacterAvatarImage = node.AvatarImagePath;
				node.Activate( PlayerController.GameObject );

				DialogueGui.Text = "";
				_currentText = node.Text;
				_printedTextLength = 0;
				_isTextPrintFinished = false;
			} else
			{
				DialogueGui.Text = _currentText;
				_printedTextLength = _currentText.Length;
				_isTextPrintFinished = true;
			}
		};

		base.OnStart();
	}

	public void ShowDialogueGui()
	{
		DialogueGui.IsShown = true;
		PlayerController.IsFreezed = true;
	}

	public void HideDialogueGui()
	{
		DialogueGui.IsShown = false;
		PlayerController.IsFreezed = false; 
	}
	
	protected override void OnUpdate()
	{
		if ( _timeElapsed > _deltaTime )
		{
			_isTextPrintFinished = false;

			if ( _currentText == null )
			{
				return;
			}

			if ( _printedTextLength < _currentText.Length )
			{
				DialogueGui.Text += _currentText[_printedTextLength];
			}
			else
			{
				_isTextPrintFinished = true;
			}

			_printedTextLength += 1;

			_timeElapsed = 0;
		}

		_timeElapsed += Time.Delta;
	}
}
