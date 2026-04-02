# Utilities Agent Guide

Helpers and builders to streamline ECS usage.

## Patterns
### 1. Fluent Builders
Use `WorldBuilder` and `EntityBuilder` to simplify initialization.
```csharp
// WorldBuilder example
var world = new WorldBuilder()
    .AddDefaultSystems()
    .Build();

// EntityBuilder example
var e = new EntityBuilder("Box")
    .AtPosition(new Vector3(10, 0, 0))
    .Build();
```

### 2. Asset Management
`AssetManager` (Singleton) handles loading and caching `Texture2D`, `SoundEffect`, etc. Always check `AssetManager` before loading assets manually.

## Key Files
- `WorldBuilder.cs`: Orchestrates world setup.
- `EntityBuilder.cs`: Fluid interface for entity creation.
- `AssetManager.cs`: Resource lifecycle management.
