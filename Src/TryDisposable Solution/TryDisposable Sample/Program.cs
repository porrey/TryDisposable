//
// Copyright(C) 2019-2025, Daniel M. Porrey. All rights reserved.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see http://www.gnu.org/licenses/.
// 
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Program
	{
		static async Task Main(string[] args)
		{
			//
			// An object that cannot be disposed.
			//
			object obj = new object();
			await obj.TryDisposeAsync();

			//
			// An object that can be disposed.
			//
			ISomeThing someThing = new SomeThing();
			await someThing.TryDisposeAsync();

#if (NET5_0 || NET6_0)
			//
			// An object that can be disposed.
			//
			ISomeAsyncThing someAsyncThing = new SomeAsyncThing();
			await someAsyncThing.TryDisposeAsync();
#endif

			//
			// Wrap the ITemporaryFolder in a using statement.
			//
			ITemporaryFolder tempFolder1 = TemporaryFolderFactory.Create1();

			using (ITryDisposable<ITemporaryFolder> disposableTempFolder = TryDisposableFactory.Create(tempFolder1))
			{
				string path = disposableTempFolder.Instance.Path;

				//
				// If tempFolder is disposable, it will get disposed, otherwise it will be ignored.
				//
			}

			//
			// Wrap the ITemporaryFolder in a using statement (non generic interface).
			//
			ITemporaryFolder tempFolder2 = TemporaryFolderFactory.Create2();

			using (ITryDisposable disposableTempFolder = TryDisposableFactory.Create(tempFolder2))
			{
				//
				// If tempFolder is disposable, it will get disposed, otherwise it will be ignored.
				//
			}
		}
	}
}