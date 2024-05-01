using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkHellperIRG
{
	public class CountTask
	{
		public int Id { get; set; }
		public string ResolutionDateFact { get; set; }

		public CountTask(int id, string resolutionDateFact)
		{
			Id = id;
			ResolutionDateFact = resolutionDateFact;
		}
	}
}
