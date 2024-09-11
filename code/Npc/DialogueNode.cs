using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Npc
{
	public class DialogueNode
	{
		public string AvatarImagePath { get; set; }
		public string CharacterName { get; set; }
		public string Text { get; set; }
		public List<DialogueNode> Children { get; set; }

		public delegate void NodeActivatedHandler( GameObject player );
		public event NodeActivatedHandler OnNodeActivated;

		public void Activate( GameObject player )
		{
			if (OnNodeActivated != null)
			{
				OnNodeActivated( player );
			}
		}
	}
}
