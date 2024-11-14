//
// Copyright(C) 2017-2025, Daniel M. Porrey. All rights reserved.
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
#if (NET5_0 || NET6_0)
using System;

namespace ConsoleApp1
{
	public interface ISomeAsyncThing
	{
		//
		// Does not implement IAsyncDispose because not all concrete
		// implementations of this interface need to be disposed.
		//
	}

	public class SomeAsyncThing : AsyncDisposableObject, ISomeAsyncThing
	{
		protected override void OnDisposeManagedObjects()
		{
			base.OnDisposeManagedObjects();
		}
	}
}
#endif