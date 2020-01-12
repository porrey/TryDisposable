using System;

namespace ConsoleApp1
{
	public interface ISomeThing
	{
	}

	public class SomeThing : DisposableObject, ISomeThing
	{
		protected override void OnDisposeManagedObjects()
		{
			base.OnDisposeManagedObjects();
		}
	}
}
