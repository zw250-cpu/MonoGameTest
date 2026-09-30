# MonoGame ECS Project

A high-performance, Unity-inspired **Entity Component System (ECS)** architecture built on **MonoGame** and **.NET 9.0**. This project is designed to provide a clean separation between game data (Components) and game logic (Systems), making it easy to scale and maintain complex game worlds.

## Key Features

*   **Unity-like Transform System**: Intuitive `TransformComponent` with support for local/world space transformations, direction vectors (Forward, Up, Right), and matrix calculations.
*   **Fluent Builder API**: Easily construct complex entities and game worlds using the `EntityBuilder` and `WorldBuilder` patterns.
*   **Centralized Resource Management**: Caching and loading of assets (textures, models, sounds) via a singleton `AssetManager`.
*   **Modular Systems**: Decoupled game logic including:
    *   **InputSystem**: Translates user hardware input into component states.
    *   **PhysicsSystem**: Handles movement integration and velocity/acceleration.
    *   **CollisionSystem**: 2D AABB collision detection and resolution.
    *   **SpriteRenderSystem**: Batch-optimized 2D rendering with camera support.
*   **AI-Ready**: Specialized `AGENTS.md` documentation in each folder to guide AI coding assistants in maintaining the project's architectural integrity.

## Project Structure

```text
MonoGameTest/
├── Components/      # Data-only classes (POCOs)
├── Entities/        # Game object blueprints and composition
├── Systems/         # Logic and rendering implementations
├── Utilities/       # Builders, Asset Management, and Helpers
├── Content/         # Game assets (Sprites, Models, etc.)
└── World.cs         # The central ECS registry and orchestrator
```

## Getting Started

### Prerequisites
*   [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
*   [MonoGame Framework](https://www.monogame.net/)

### Building the Project
```bash
dotnet restore
dotnet build
```

### Creating an Entity
```csharp
var player = new EntityBuilder("Hero")
    .AtPosition(new Vector3(100, 100, 0))
    .AddComponent(new Sprite2DComponent(texture))
    .AddComponent(new VelocityComponent())
    .Build();

world.AddEntity(player);
```

### Adding a System
```csharp
var world = new WorldBuilder()
    .AddSystem(new InputSystem { Priority = 0 })
    .AddSystem(new PhysicsSystem { Priority = 1 })
    .AddSystem(new SpriteRenderSystem { Priority = 10 })
    .Build();
```

