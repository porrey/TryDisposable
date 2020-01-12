using System;
using System.Threading.Tasks;

#pragma warning disable DF0010

namespace ConsoleApp1
{
	class Program
	{
		static async Task Main(string[] args)
		{
			// an object that cannot be disposed.
			object obj = new object();
			await obj.TryDisposeAsync();

			// an object that can be disposed.
			ISomeThing someThing = new SomeThing();
			await someThing.TryDisposeAsync();

			// wrap the ITemporaryFolder in a using statement.
			ITemporaryFolder tempFolder1 = TemporaryFolderFactory.Create1();

			using (ITryDisposable<ITemporaryFolder> disposableTempFolder = TryDisposableFactory.Create(tempFolder1))
			{
				string path = disposableTempFolder.Instance.Path;

				// If tempFolder is disposable, it will get disposed, otherwise it will be ignored.
			}

			// wrap the ITemporaryFolder in a using statement (non generic interface).
			ITemporaryFolder tempFolder2 = TemporaryFolderFactory.Create2();

			using (ITryDisposable disposableTempFolder = TryDisposableFactory.Create(tempFolder2))
			{
				// If tempFolder is disposable, it will get disposed, otherwise it will be ignored.
			}
		}
	}
}

#pragma warning restore DF0010