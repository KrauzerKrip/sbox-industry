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
			DialogueNode rootNode = new()
			{
				Text = "Hello! I see you are new here. I'm a local trader, and I sell blueprints.",
				CharacterName = "Blueprint trader"
			};
			DialogueNode node1 = new()
			{
				Text = "Wait, don't you know what are those blueprints are?",
				CharacterName = "Blueprint trader"
			};
			DialogueNode node2 = new()
			{
				Text = "They are needed in the case you don't know how to build a machine. You can't build something meaningful without a plan! So, I'm here to help you, and support the development of your own factory!",
				CharacterName = "Blueprint trader"
			};
			DialogueNode node3 = new()
			{
				Text = "Anyway, come back when you will have some money. Bye!",
				CharacterName = "Blueprint trader"
			};

			rootNode.Children.Add( node1 );
			node1.Children.Add( node2 );
			node2.Children.Add( node3 );

			DialogueTree firstDialogueTree = new();
			firstDialogueTree.Root = rootNode;

			Tree = firstDialogueTree;
		}
	}
}
