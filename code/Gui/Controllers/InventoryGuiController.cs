using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Gui.Controllers
{
	public class InventoryGuiController : Component
	{
		[Property]
		public Inventory Inventory { get; set; }
		[Property]
		public InventoryGui InventoryGui { get; set; }
		[Property]
		public PlayerController PlayerController { get; set; }
		[Property]
		public string DraggedItem { get; set; }
		[Property]
		public float DraggedItemAmount { get; set; }

		protected override void OnStart()
		{
			InventoryGui.OnItemClick += (string itemName) => {
				if ( DraggedItem == itemName )
				{
					DraggedItem = null;
					DraggedItemAmount = 0;
				} else
				{
					DraggedItem = itemName;
					DraggedItemAmount = Inventory.Resources[itemName];
				}
			};

			InventoryGui.OnRightClick += () =>
			{
				if ( DraggedItem != null )
				{
					DraggedItemAmount /= 2;
				}
			};

			base.OnStart();
		}

		protected override void OnUpdate()
		{
			if ( PlayerController.IsInventoryOpened )
			{
				InventoryGui.IsShown = true;
			}
			else
			{
				InventoryGui.IsShown = false;
			}

			if ( PlayerController.IsGuiOpened )
			{
				InventoryGui.IsCompact = true;
			} else
			{
				InventoryGui.IsCompact = false;
			}

			if ( !PlayerController.IsInventoryOpened )
			{
				DraggedItem = null;
				DraggedItemAmount = 0;
			}

			InventoryGui.Resources = Inventory.Resources.Clone();

			if ( DraggedItem != null && DraggedItemAmount != 0) {
				if ( InventoryGui.Resources.ContainsKey( DraggedItem ) && Inventory.Resources.ContainsKey( DraggedItem ) )
				{
					InventoryGui.DraggedItemName = DraggedItem;
					InventoryGui.DraggedItemAmount = DraggedItemAmount;
					InventoryGui.DraggedItemPosX = Mouse.Position.x;
					InventoryGui.DraggedItemPosY = Mouse.Position.y;
					InventoryGui.IsDraggedItemShown = true;

					InventoryGui.Resources[DraggedItem] = Inventory.Resources[DraggedItem] - DraggedItemAmount;
				}
			} else
			{
				InventoryGui.IsDraggedItemShown = false;
			}

			base.OnUpdate();
		}
	}
}
