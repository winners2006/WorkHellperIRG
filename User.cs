namespace WorkHellperIRG
{
	internal class User
	{
		public string Name { get; }
		public string Id { get; set; }
		public User(string name, string id)
		{
			Name = name;
			Id = id;
		}
	}
}