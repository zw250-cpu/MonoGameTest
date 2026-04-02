# Coding Agent Guide: MonoGame ECS Structure

This project implements a Unity-inspired Entity Component System (ECS) to separate data and logic. All game state and logic execution are managed by the `World` instance in `World.cs`.

## Architectural Rules
- **Entities (`Entities/Entity.cs`)**: Containers for components. **Every entity has a `TransformComponent` by default.** Access it via `entity.Transform`. Use `entity.AddComponent<T>(new T())` and `entity.GetComponent<T>()` where `T` must inherit from `BaseComponent`.
- **Components (`Components/*.cs`)**: All components **must** inherit from `BaseComponent`. They should be data-only classes (POCOs), with logic limited to data helpers (e.g., `TransformComponent.WorldMatrix`).
- **Systems (`Systems/*.cs`)**: All game logic resides here. Inherit from `BaseSystem` and implement `Update` (for logic) or `Draw` (for rendering).
- **World (`World.cs`)**: Central registry. Use `WorldBuilder` to initialize the world and its systems.

## Core Patterns
### 1. Implementing New Logic
Create a system in `Systems/` (e.g., `CollisionSystem.cs`) that inherits from `BaseSystem`. Filter entities by required components:
```csharp
public override void Update(GameTime gameTime, IEnumerable<Entity> entities) {
    foreach (var entity in entities.Where(e => e.HasComponent<BoxCollider2DComponent>())) {
        var transform = entity.Transform; // Already guaranteed to exist
        // Implementation
    }
}
```

### 2. Defining Game Objects
There are two primary ways to create entities:
1. **Dedicated Entity Classes**: Inherit from `Entity` (e.g., `Entities/PlayerEntity.cs`) and initialize components in the constructor.
2. **EntityBuilder**: Use the fluent `MonoGameTest.Utilities.EntityBuilder` for generic or one-off entities.
   ```csharp
   var enemy = new EntityBuilder("Enemy")
       .AtPosition(new Vector3(100, 100, 0))
       .AddComponent(new Sprite2DComponent(texture))
       .Build();
   ```

### 3. Rendering Flow
Systems with rendering logic must implement `Draw`. The `SpriteRenderSystem` is the primary 2D renderer. Ensure rendering systems are added with a higher `Priority` than logic systems to maintain the correct update/draw order.

## Integration Points
- **System Priority**: Set `Priority` during `AddSystem()` to control execution order (e.g., `Input` -> `Physics` -> `Collision` -> `Render`).
- **Asset Loading**: Use `AssetManager.Instance.Load<T>("assetName")` to load and cache assets. Initialize it in `MonoGameTest.LoadContent` with `AssetManager.Initialize(Content)`.
- **MonoGameTest.cs**: The entry point where the `World`, `Systems`, and initial `Entities` are wired together.

## Directory Structure
- `Components/`: State definitions (e.g., `TransformComponent`, `VelocityComponent`).
- `Entities/`: Specific entity blueprints (e.g., `PlayerEntity`).
- `Systems/`: Logic and rendering implementations (e.g., `MovementSystem`, `SpriteRenderSystem`).
- `Utilities/`: Helper classes (e.g., `WorldBuilder.cs`, `EntityBuilder.cs`).
- `World.cs`: Coordinates the `Entity` and `System` lifecycles.
