using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Npc
{
	public class Npc : Component
	{
		[Property]
		public DialogueManager DialogueManager { get; set; }

		protected override void OnFixedUpdate()
		{
			base.OnFixedUpdate();
		}

		protected void StartDialogue( GameObject player, Dialogue dialogue )
		{
			DialogueManager.StartDialogue( player, dialogue );
		}
	}
}
