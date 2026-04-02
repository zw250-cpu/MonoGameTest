# Systems Agent Guide

Systems contain all game logic and rendering.

## Rules
- **Base Type**: Must inherit from `BaseSystem`.
- **Logic vs Render**: 
  - Override `Update` for game logic (movement, physics, AI).
  - Override `Draw` for rendering.
- **Filtering**: Systems should filter the provided `IEnumerable<Entity>` for the specific components they require.

## Patterns
### 1. Component Filtering
Use `entities.Where(e => e.HasComponent<T>())` to find relevant entities.
```csharp
public override void Update(GameTime gameTime, IEnumerable<Entity> entities) {
    foreach (var entity in entities.Where(e => e.HasComponent<VelocityComponent>())) {
        var velocity = entity.GetComponent<VelocityComponent>();
        entity.Transform.Position += velocity.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}
```

### 2. Priority
Set the `Priority` property to control execution order. Lower numbers run first.
- Input/Physics: 0-5
- Collision: 6-9
- Rendering: 10+

## Key Files
- `BaseSystem.cs`: Defines the system lifecycle.
- `SpriteRenderSystem.cs`: The primary 2D renderer.
- `PhysicsSystem.cs`: Handles movement integration.
