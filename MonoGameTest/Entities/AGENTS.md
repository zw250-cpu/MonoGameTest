# Entities Agent Guide

Entities are containers for components.

## Rules
- **Base Type**: Inherit from `Entity.cs` for specific blueprints.
- **Default Transform**: The base `Entity` constructor automatically adds a `TransformComponent`.
- **Registration**: Entities must be added to the `World` instance to be processed by systems.

## Patterns
### 1. Specialized Entity
Initialize components in the constructor. Use `AddComponent<T>` to set up the starting state.
```csharp
public class PlayerEntity : Entity {
    public PlayerEntity(Texture2D tex) : base() {
        Name = "Player";
        AddComponent(new Sprite2DComponent(tex));
        AddComponent(new VelocityComponent());
    }
}
```

### 2. Dynamic Creation
Use `EntityBuilder` (in `Utilities/`) for generic entities or when building from data.

## Key Files
- `Entity.cs`: Core container logic.
- `PlayerEntity.cs`: Example of a specialized blueprint.
