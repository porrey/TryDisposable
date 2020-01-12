using System;

namespace ConsoleApp1
{
	public interface ISomeThing
	{
		// does not implement IDispose because not all concrete
		// implementations of this interface need to be disposed.
	}

	public class SomeThing : DisposableObject, ISomeThing
	{
		protected override void OnDisposeManagedObjects()
		{
			base.OnDisposeManagedObjects();
		}
	}
}
