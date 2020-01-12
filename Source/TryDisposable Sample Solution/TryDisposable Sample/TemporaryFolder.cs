using System;

#pragma warning disable DF0100

namespace ConsoleApp1
{
	public interface ITemporaryFolder
	{
		string Path { get; set; }
	}

	public class TemporaryFolder1: DisposableObject, ITemporaryFolder
	{
		public string Path { get; set; }

		protected override void OnDisposeManagedObjects()
		{
			base.OnDisposeManagedObjects();
		}
	}

	public class TemporaryFolder2 : ITemporaryFolder
	{
		public string Path { get; set; }
	}

	public static class TemporaryFolderFactory
	{
		public static ITemporaryFolder Create1()
		{
			return new TemporaryFolder1();
		}

		public static ITemporaryFolder Create2()
		{
			return new TemporaryFolder2();
		}
	}
}

#pragma warning restore DF0100