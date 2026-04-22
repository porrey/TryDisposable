![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/porrey/TryDisposable/.github%2Fworkflows%2Fdotnet.yml?style=for-the-badge&label=Build%20and%20Test) ![GitHub License](https://img.shields.io/github/license/porrey/TryDisposable?style=for-the-badge) ![.NET](https://img.shields.io/badge/.NET-10-purple?style=for-the-badge)

[![Nuget](https://img.shields.io/nuget/v/TryDisposable?label=TryDisposable%20-%20NuGet&style=for-the-badge)![Nuget](https://img.shields.io/nuget/dt/TryDisposable?label=Downloads&style=for-the-badge)](https://www.nuget.org/packages/TryDisposable/)

# TryDisposable

Wrap an object in a disposable decorator to attempt to dispose it later. This is useful when retrieving an instance from a factory or dependency-injection container through an interface that does not extend `IDisposable` or `IAsyncDisposable` — even though the concrete class does. TryDisposable lets you dispose the object safely and consistently, without needing to check for the interface yourself and cast.

---

## Table of Contents

- [Installation](#installation)
- [The Problem](#the-problem)
- [Synchronous API](#synchronous-api)
  - [TryDispose() extension](#trydispose-extension)
  - [TryDisposable wrapper](#trydisposable-wrapper)
  - [TryDisposableFactory](#trydisposablefactory)
  - [TryDisposable static helpers](#trydisposable-static-helpers)
- [Asynchronous API](#asynchronous-api)
  - [TryDisposeAsync() extension](#trydisposeasync-extension)
  - [TryAsyncDisposable wrapper](#tryasyncdisposable-wrapper)
  - [TryAsyncDisposableFactory](#tryasyncdisposablefactory)
  - [TryAsyncDisposable static helpers](#tryasyncdisposable-static-helpers)
- [Interface Reference](#interface-reference)
- [License](#license)

---

## Installation

Install the package from NuGet:

```
dotnet add package TryDisposable
```

All types are placed in the `System` namespace, so no additional `using` directive is needed.

---

## The Problem

Consider a common factory or DI pattern:

```csharp
public interface ITemporaryFolder
{
    string Path { get; }
}

// The concrete class is disposable, but the interface is not.
public class TemporaryFolder : ITemporaryFolder, IDisposable
{
    public string Path { get; set; }
    public void Dispose() { /* clean up the folder */ }
}
```

When you hold the instance as `ITemporaryFolder`, you cannot call `Dispose()` directly. The traditional work-around is:

```csharp
ITemporaryFolder folder = factory.Create();
try
{
    // use folder...
}
finally
{
    (folder as IDisposable)?.Dispose();
}
```

TryDisposable removes this boilerplate by providing a clean, reusable wrapper.

---

## Synchronous API

### TryDispose() extension

The simplest way to attempt to dispose any object. The extension method is available on every type.

```csharp
// Works on any object — does nothing if it is not IDisposable.
object obj = new();
obj.TryDispose();

// Works through a non-disposable interface reference.
ISomeThing thing = new SomeThing();   // SomeThing implements IDisposable
thing.TryDispose();                   // Dispose() is called on the concrete class
```

### TryDisposable wrapper

Wraps an object so it can be used in a `using` statement regardless of whether the interface exposes `IDisposable`.

```csharp
ITemporaryFolder folder = factory.Create();

using (var wrapper = new TryDisposable<ITemporaryFolder>(folder))
{
    string path = wrapper.Instance.Path;
    // ...
    // folder.Dispose() is called when the using block exits (if IDisposable).
}
```

### TryDisposableFactory

The recommended way to create a wrapper. Returns an `ITryDisposable<T>` or the non-generic `ITryDisposable`.

```csharp
// Generic — gives access to the underlying instance via .Instance
ITemporaryFolder folder = factory.Create();

using (ITryDisposable<ITemporaryFolder> wrapper = TryDisposableFactory.Create(folder))
{
    string path = wrapper.Instance.Path;
}

// Non-generic — when you do not need to access the wrapped object
using (ITryDisposable wrapper = TryDisposableFactory.Create(folder))
{
    // folder will be disposed at the end of the block if it is IDisposable
}

// Async factory method (returns a completed Task)
ITryDisposable<ITemporaryFolder> wrapper = await TryDisposableFactory.CreateAsync(folder);
```

### TryDisposable static helpers

```csharp
// Dispose a typed instance directly (no wrapper needed)
TryDisposable<ITemporaryFolder>.Dispose(folder);

// Async dispose a typed instance directly
await TryDisposable<ITemporaryFolder>.DisposeAsync(folder);
```

---

## Asynchronous API

### TryDisposeAsync() extension

Available on every type. Prefers `IAsyncDisposable`, falls back to `IDisposable`, and does nothing if neither is implemented.

```csharp
// Works on any object.
object obj = new();
await obj.TryDisposeAsync();

// Calls DisposeAsync() if available.
ISomeAsyncThing thing = new SomeAsyncThing();   // implements IAsyncDisposable
await thing.TryDisposeAsync();

// Falls back to Dispose() if only IDisposable is implemented.
ISomeThing syncThing = new SomeThing();         // implements IDisposable only
await syncThing.TryDisposeAsync();
```

### TryAsyncDisposable wrapper

Wraps an object for use in an `await using` statement.

```csharp
ISomeAsyncThing thing = factory.Create();

await using (var wrapper = new TryAsyncDisposable<ISomeAsyncThing>(thing))
{
    _ = wrapper.Instance;
    // ...
    // DisposeAsync() (or Dispose()) is called when the block exits.
}
```

### TryAsyncDisposableFactory

```csharp
// Generic wrapper
await using (ITryAsyncDisposable<ISomeAsyncThing> wrapper = TryAsyncDisposableFactory.Create(thing))
{
    _ = wrapper.Instance;
}

// Non-generic wrapper
await using (ITryAsyncDisposable wrapper = TryAsyncDisposableFactory.Create(thing))
{
}

// Async factory
ITryAsyncDisposable<ISomeAsyncThing> wrapper = await TryAsyncDisposableFactory.CreateAsync(thing);
```

### TryAsyncDisposable static helpers

```csharp
// Async dispose a typed instance directly
await TryAsyncDisposable<ISomeAsyncThing>.DisposeAsync(thing);
```

---

## Interface Reference

| Type | Description |
|---|---|
| `ITryDisposable` | Non-generic marker interface extending `IDisposable`. |
| `ITryDisposable<T>` | Generic interface extending `ITryDisposable`; exposes `Instance`. |
| `TryDisposable<T>` | Concrete wrapper class implementing `ITryDisposable<T>`. |
| `TryDisposableFactory` | Static factory for creating `ITryDisposable<T>` instances. |
| `TryDisposableExtensions` | Extension methods: `TryDispose()`. |
| `ITryAsyncDisposable` | Non-generic marker interface extending `IAsyncDisposable`. |
| `ITryAsyncDisposable<T>` | Generic interface extending `ITryAsyncDisposable`; exposes `Instance`. |
| `TryAsyncDisposable<T>` | Concrete wrapper class implementing `ITryAsyncDisposable<T>`. |
| `TryAsyncDisposableFactory` | Static factory for creating `ITryAsyncDisposable<T>` instances. |
| `TryAsyncDisposableExtensions` | Extension methods: `TryDisposeAsync()`. |

---

## License

Copyright © Daniel M. Porrey 2017–2026.  
Licensed under the [GNU Lesser General Public License v3.0 or later](LICENSE).
