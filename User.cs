namespace WorkHellperIRG
{
	internal class User
	{
		public string Name { get; }
		public int Id { get; set; }
		public User(string name, int id)
		{
			Name = name;
			Id = id;
		}
	}
}