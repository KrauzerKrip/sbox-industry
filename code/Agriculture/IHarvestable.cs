using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Agriculture
{
	public interface IHarvestable
	{
		void Harvest( GameObject harvester );
	}
}
