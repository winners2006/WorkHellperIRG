using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkHellperIRG
{
	public class NewTask
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Deadline { get; set; }
		public string PriorityId { get; set; }
		public string Created { get; set; }

		public NewTask(int id, string name, string deadline, string priorityId, string created)
		{
			Id = id;
			Name = name;
			Deadline = deadline;
			PriorityId = priorityId;
			Created = created;
		}
	}
}
