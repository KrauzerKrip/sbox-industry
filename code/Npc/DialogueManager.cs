using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Npc
{
	public class DialogueManager : Component
	{
		public delegate void DialogueStartHandler( GameObject player, Dialogue dialogue );
		public event DialogueStartHandler OnDialogueStart;

		public void StartDialogue( GameObject player, Dialogue dialogue )
		{
			if ( OnDialogueStart != null ) {
				OnDialogueStart( player, dialogue );
			}
		}
	}
}
