using System;
using System.Collections.Generic;
using MonoGameTest.Components;

namespace MonoGameTest.Entities;

public class Entity
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; } = "New Entity";
    
    // Every entity has a transform by default
    public TransformComponent Transform { get; }
    
    private readonly Dictionary<Type, BaseComponent> _components = new();

    public Entity()
    {
        Transform = new TransformComponent();
        AddComponent(Transform);
    }

    public T AddComponent<T>(T component) where T : BaseComponent
    {
        var type = typeof(T);
        if (!_components.TryAdd(type, component))
        {
            // If it's the default transform being added, we just return it
            return component == Transform ? component : throw new Exception($"Component of type {type.Name} already exists on entity {Name}.");
        }

        return component;
    }

    public T GetComponent<T>() where T : BaseComponent
    {
        if (_components.TryGetValue(typeof(T), out var component))
        {
            return (T)component;
        }
        return null;
    }

    public bool HasComponent<T>() where T : BaseComponent
    {
        return _components.ContainsKey(typeof(T));
    }

    public bool RemoveComponent<T>() where T : BaseComponent
    {
        return typeof(T) != typeof(TransformComponent) && _components.Remove(typeof(T));
    }

    public IEnumerable<BaseComponent> GetAllComponents() => _components.Values;
}
