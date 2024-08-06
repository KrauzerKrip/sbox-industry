using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Tools
{
	public abstract class ToolBase : Component
	{
		public string Name { get; protected set; }

		/// <summary>
		/// Usually left mouse button
		/// </summary>
		public abstract void Attack1();
		/// <summary>
		/// Usually right mouse button
		/// </summary>
		public abstract void Attack2();
		/// <summary>
		/// Usually R
		/// </summary>
		public abstract void Reload();
	}
}
