using Sandbox.Npc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Gui.Controllers
{
	public class DialogueHandler
	{
		private Dialogue _dialogue;
		private DialogueNode _currentDialogueNode;

		public DialogueHandler(Dialogue dialogue) {
			_dialogue = dialogue;
		}

		public void Start()
		{
			_currentDialogueNode = _dialogue.Tree.Root;
		}

		public void Next()
		{
			if ( _currentDialogueNode == null )
			{
				return;
			}

			if ( _currentDialogueNode.Children.Count == 1 )
			{
				_currentDialogueNode = _currentDialogueNode.Children.First();
			}
		}

		public bool IsNodeLast()
		{
			if ( _currentDialogueNode.Children.Count == 0)
			{
				return true;
			}

			return false;
		}

		public DialogueNode GetCurrentNode()
		{
			return _currentDialogueNode;
		}
	}
}
