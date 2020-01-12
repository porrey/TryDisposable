namespace ConsoleApp1
{
	public interface ITemporaryFolder
	{
		string Path { get; set; }
	}

	public class TemporaryFolder: ITemporaryFolder
	{
		public string Path { get; set; }

		public static class Factory
		{
			public static ITemporaryFolder Create()
			{
				return new TemporaryFolder();
			}
		}
	}
}
