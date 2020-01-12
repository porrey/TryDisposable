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

			ITemporaryFolder tempFolder = TemporaryFolder.Factory.Create();

			using (ITryDisposable<ITemporaryFolder> disposableTempFolder = TryDisposableFactory.Create(tempFolder))
			{
				string path = disposableTempFolder.Instance.Path;

				// If tempFolder is disposable, it will get disposed, otherwise it will be ignored.
			}

			using (ITryDisposable disposableTempFolder = TryDisposableFactory.Create(tempFolder))
			{
				// If tempFolder is disposable, it will get disposed, otherwise it will be ignored.
			}
		}
	}
}

#pragma warning restore DF0010