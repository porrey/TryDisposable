# TryDisposable — Architecture

## Overview

**TryDisposable** is a small, focused .NET library that solves a common problem with the disposal pattern: when an object is retrieved through an interface that does not extend `IDisposable` or `IAsyncDisposable`, there is no type-safe way to dispose it without a manual cast. This library provides a lightweight decorator that wraps any object and attempts to dispose it at the end of a `using` block, silently doing nothing if the concrete type does not implement a disposable interface.

---

## Repository Layout

```
TryDisposable/
├── Images/                          # NuGet package icon
├── Src/
│   ├── TryDisposable Solution.sln   # Visual Studio solution
│   ├── TryDisposable/               # Main library project (net10.0)
│   │   ├── Standard/                # Synchronous disposal API
│   │   │   ├── ITryDisposable.cs
│   │   │   ├── TryDisposable.cs
│   │   │   ├── TryDisposableExtensions.cs
│   │   │   └── TryDisposableFactory.cs
│   │   ├── Async/                   # Asynchronous disposal API
│   │   │   ├── ITryAsyncDisposable.cs
│   │   │   ├── TryAsyncDisposable.cs
│   │   │   ├── TryAsyncDisposableExtensions.cs
│   │   │   └── TryAsyncDisposableFactory.cs
│   │   ├── XmlDocs/
│   │   │   └── TryDisposable.xml    # Generated XML documentation
│   │   └── TryDisposable.csproj
│   ├── TryDisposable Sample/        # Console application demonstrating usage
│   │   ├── Program.cs
│   │   ├── SomeThing.cs
│   │   ├── SomeAsyncThing.cs
│   │   ├── TemporaryFolder.cs
│   │   └── TryDisposable Sample.csproj
│   └── TryDisposable Tests/         # xUnit test project
│       └── TryDisposable Tests.csproj
├── README.md
└── ARCHITECTURE.md
```

All public types are placed in the `System` namespace so that they are available everywhere without an additional `using` directive.

---

## Key Technologies

| Technology | Role |
|---|---|
| .NET 10 (`net10.0`) | Target framework |
| C# 13 | Implementation language |
| xUnit | Unit testing framework |
| NuGet | Package distribution |
| GitHub Actions | CI/CD (build and test) |

---

## API Surface

### Synchronous Disposal (`Standard/`)

#### `ITryDisposable` / `ITryDisposable<TUnderlyingType>`
Two interfaces that extend `IDisposable`. The generic variant adds an `Instance` property to surface the wrapped object. Both allow the wrapper to be used in a `using` statement.

#### `TryDisposable<TUnderlyingType>`
The core implementation class. Wraps any object and forwards `Dispose()` to `TryDispose()` (the extension method). If the underlying object does not implement `IDisposable`, the call is a no-op.

Notable static helpers on the class:
- `Dispose<TItem>(instance)` – disposes a typed instance without needing a wrapper.
- `DisposeAsync<TItem>(instance)` – asynchronously disposes a typed instance; returns `ValueTask`.

#### `TryDisposableFactory`
Static factory with two methods:
- `Create<TItem>(instance)` → `ITryDisposable<TItem>` (synchronous)
- `CreateAsync<TItem>(instance)` → `Task<ITryDisposable<TItem>>` (returns an already-completed task)

#### `TryDisposableExtensions`
Extension methods added to every type (`this TItem item`):
- `TryDispose()` – casts to `IDisposable` and calls `Dispose()` if successful.

---

### Asynchronous Disposal (`Async/`)

The async API mirrors the synchronous one but targets `IAsyncDisposable`.

#### `ITryAsyncDisposable` / `ITryAsyncDisposable<TUnderlyingType>`
Interfaces that extend `IAsyncDisposable`.

#### `TryAsyncDisposable<TUnderlyingType>`
Wraps any object and forwards `DisposeAsync()` to `TryDisposeAsync()` (the extension method). Returns a `ValueTask`.

Static helpers:
- `DisposeAsync<TItem>(instance)` → `ValueTask`

#### `TryAsyncDisposableFactory`
- `Create<TItem>(instance)` → `ITryAsyncDisposable<TItem>`
- `CreateAsync<TItem>(instance)` → `Task<ITryAsyncDisposable<TItem>>`

#### `TryAsyncDisposableExtensions`
- `TryDisposeAsync()` – tries `IAsyncDisposable` first, then falls back to `IDisposable`, then does nothing. Returns `ValueTask`.

---

## Design Decisions

### Single target framework (`net10.0`)
The library targets only `net10.0`. This eliminates the need for multi-targeting compatibility shims. All compiler directives (`#if NET10_0`, `#if !NET10_0`) that were present historically are unnecessary and have been removed.

### `System` namespace
Placing types in `System` avoids requiring consumers to add a `using` statement. Extension methods on `TItem` (which is unconstrained) are available on every object.

### Separate sync/async hierarchies
`ITryDisposable` → `IDisposable` and `ITryAsyncDisposable` → `IAsyncDisposable` are kept as separate, independent hierarchies. This mirrors the BCL approach and avoids a diamond-inheritance situation where a class would need to implement both `Dispose()` and `DisposeAsync()`.

### Null guard in constructors
Both `TryDisposable<T>` and `TryAsyncDisposable<T>` throw `ArgumentNullException` when constructed with a `null` instance, providing a clear and immediate error rather than a deferred `NullReferenceException` at disposal time.

### Dispose-once guard
Both wrapper classes maintain a private `_disposed` flag so that calling `Dispose()` or `DisposeAsync()` more than once is idempotent, conforming to the recommended dispose pattern.

---

## Data Flow

### Synchronous path
```
Consumer calls using (TryDisposableFactory.Create(obj))
  └─> TryDisposable<T>.Dispose()
        └─> TryDisposableExtensions.TryDispose(this.Instance)
              └─> (Instance as IDisposable)?.Dispose()
```

### Asynchronous path
```
Consumer calls await using (TryAsyncDisposableFactory.Create(obj))
  └─> TryAsyncDisposable<T>.DisposeAsync()
        └─> TryAsyncDisposableExtensions.TryDisposeAsync(this.Instance)
              ├─> if IAsyncDisposable → asyncDisposable.DisposeAsync()
              ├─> else if IDisposable → disposable.Dispose(); return ValueTask.CompletedTask
              └─> else → return ValueTask.CompletedTask
```

---

## CI/CD

The GitHub Actions workflow (`.github/workflows/dotnet.yml`) runs on every push and pull request to `master`:
1. Restores NuGet dependencies.
2. Builds the solution.
3. Runs all tests (`dotnet test`).
