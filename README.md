![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/porrey/TryDisposable/.github%2Fworkflows%2Fdotnet.yml?style=for-the-badge&label=Build%20and%20Test) ![GitHub License](https://img.shields.io/github/license/porrey/TryDisposable?style=for-the-badge) ![.NET](https://img.shields.io/badge/.NET-10-purple?style=for-the-badge)

[![Nuget](https://img.shields.io/nuget/v/TryDisposable?label=TryDisposable%20-%20NuGet&style=for-the-badge)![Nuget](https://img.shields.io/nuget/dt/TryDisposable?label=Downloads&style=for-the-badge)](https://www.nuget.org/packages/TryDisposable/)

# TryDisposable
Wrap an object in a disposable decorator to attempt to dispose the object later. This is useful when retrieving an instance of an object from a factory or container while only having an interface reference. If the interface  does not implement IDisposable, but the concrete class does, this will allow the instance to be disposed.

##Example 1
	
Use the simple TryDispose() method available on any object.

    object obj = new object();
    obj.TryDispose();

##Example 2

Wrap an object in a using statement. Use the instance property to access the object.

    ITemporaryFolder tempFolder = TemporaryFolder.Factory.Create();
    
    using (ITryDisposable<ITemporaryFolder> disposableTempFolder = TryDisposableFactory.Create(tempFolder))
    {
    	string path = disposableTempFolder.Instance.Path;
    
    	// If tempFolder is disposable, it will get disposed, otherwise it will be ignored.
    }

##Example 2

Use the following if the underlying object does not need to be accessed through the ITryDisposbale<TUnderlyingItem> interface.
    
    using (ITryDisposable disposableTempFolder = TryDisposableFactory.Create(tempFolder))
    {
    	// If tempFolder is disposable, it will get disposed, otherwise it will be ignored.
    }
