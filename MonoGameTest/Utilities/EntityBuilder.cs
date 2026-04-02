using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameTest.Components;
using MonoGameTest.Entities;

namespace MonoGameTest.Utilities;

public class EntityBuilder
{
    private Entity _entity;

    public EntityBuilder(string name = "New Entity")
    {
        _entity = new Entity { Name = name };
    }

    public EntityBuilder AtPosition(Vector3 position)
    {
        _entity.Transform.Position = position;
        return this;
    }

    public EntityBuilder WithRotation(Vector3 rotation)
    {
        _entity.Transform.Rotation = rotation;
        return this;
    }

    public EntityBuilder WithScale(Vector3 scale)
    {
        _entity.Transform.Scale = scale;
        return this;
    }

    public EntityBuilder AddComponent<T>(T component) where T : BaseComponent
    {
        _entity.AddComponent(component);
        return this;
    }

    /// <summary>
    /// Preset: Adds a solid color Sprite, Collider, and Rigidbody.
    /// </summary>
    public EntityBuilder WithSolidColor(Color color, Vector2 size, bool isKinematic = false)
    {
        var texture = AssetManager.Instance.WhitePixel;
        _entity.AddComponent(new Sprite2DComponent(texture) 
        { 
            Color = color,
            Origin = new Vector2(0.5f, 0.5f) // Center the 1x1 pixel
        });
        
        // Since the texture is 1x1, we must scale the Transform to match the desired size
        _entity.Transform.Scale = new Vector3(size.X, size.Y, 1);
        
        _entity.AddComponent(new BoxCollider2DComponent(new Vector2(1, 1))); 
        _entity.AddComponent(new RigidbodyComponent { IsKinematic = isKinematic });
        return this;
    }

    /// <summary>
    /// Preset: Adds Sprite, Collider, and Rigidbody in one go using an asset name.
    /// </summary>
    public EntityBuilder WithPhysicsSprite(string assetName, Vector2 colliderSize, bool isKinematic = false)
    {
        var texture = AssetManager.Instance.Load<Texture2D>(assetName);
        return WithPhysicsSprite(texture, colliderSize, isKinematic);
    }

    /// <summary>
    /// Preset: Adds Sprite, Collider, and Rigidbody in one go.
    /// </summary>
    public EntityBuilder WithPhysicsSprite(Texture2D texture, Vector2 colliderSize, bool isKinematic = false)
    {
        _entity.AddComponent(new Sprite2DComponent(texture));
        _entity.AddComponent(new BoxCollider2DComponent(colliderSize));
        _entity.AddComponent(new RigidbodyComponent { IsKinematic = isKinematic });
        return this;
    }

    public Entity Build()
    {
        return _entity;
    }
}
