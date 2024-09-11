using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Npc.Dialogues
{
	public class BTDialogue1 : Dialogue
	{
		public BTDialogue1()
		{
			DialogueTree firstDialogueTree = new();
			DialogueNode rootNode = new()
			{
				Text = "Hello! I see you are new here. I'm a local trader, and I sell blueprints. Wait, don't you know what are those blueprints are? They are needed in the case you don't know how to build a machine. You can't build something meaningful without a plan! So, I'm here to help you, and support the development of your own factory! Anyway, come back when you will have some money. Bye!",
				CharacterName = "Blueprint trader"
			};
			firstDialogueTree.Root = rootNode;
			Tree = firstDialogueTree;
		}
	}
}
