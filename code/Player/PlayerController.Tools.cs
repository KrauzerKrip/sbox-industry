using Sandbox.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class PlayerController
{
	[Property]
	[Category("Inventory")]
	public List<ToolBase> Tools { get; set; } = new List<ToolBase>();
	private int _currentToolIndex = 0;

	public void NextTool()
	{
		_currentToolIndex++;
		
		if (_currentToolIndex >= Tools.Count)
		{
			_currentToolIndex = Tools.Count - 1;
		}
	}

	public void PrevTool()
	{
		_currentToolIndex--;

		if ( _currentToolIndex < 0)
		{
			_currentToolIndex = 0;
		}
	}

	public void SetToolIndex(int index)
	{
		if ( index < 0 )
		{
			index = 0;
		}
		if ( index >= Tools.Count )
		{
			index = Tools.Count - 1;
		}

		_currentToolIndex = index;
	}

	public ToolBase GetCurrentTool()
	{
		return Tools[ _currentToolIndex ];
	}

}
