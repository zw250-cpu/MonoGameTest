# Components Agent Guide

This folder contains the data definitions (POCOs) for the ECS.

## Rules
- **Base Type**: All components MUST inherit from `BaseComponent`.
- **Data Only**: Components should primarily store state. Avoid complex logic; put that in `Systems/`.
- **Primary Transform**: Every `Entity` has a `TransformComponent` by default. Do not add `Position` or `Rotation` to other components; reference the entity's transform instead.

## Patterns
### 1. New Component
Inherit from `BaseComponent`. Use public properties for data that systems will mutate.
```csharp
public class VelocityComponent : BaseComponent {
    public Vector3 Velocity { get; set; } = Vector3.Zero;
}
```

### 2. Constructor Injection
Use constructors for mandatory data (like textures or models).
```csharp
public class Sprite2DComponent(Texture2D texture) : BaseComponent {
    public Texture2D Texture { get; set; } = texture;
}
```

## Key Files
- `BaseComponent.cs`: The required base class.
- `TransformComponent.cs`: The most used component, provides matrix and direction helpers.
