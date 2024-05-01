namespace WorkHellperIRG
{
	public class MyTask
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Deadline { get; set; }
		public string PriorityId { get; set; }
		public string Created { get; set; }
		public string EditorId { get; set; }

		public MyTask(int id, string name, string deadline, string priorityId, string created, string editorId)
		{
			Id = id;
			Name = name;
			Deadline = deadline;
			PriorityId = priorityId;
			Created = created;
			EditorId = editorId;
		}
	}
}
