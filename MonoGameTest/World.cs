using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameTest.Entities;
using MonoGameTest.Systems;
using System.Collections.Generic;
using System.Linq;

namespace MonoGameTest;

public class World
{
    private readonly List<Entity> _entities = new();
    private readonly List<BaseSystem> _systems = new();

    public void AddEntity(Entity entity) => _entities.Add(entity);
    public void RemoveEntity(Entity entity) => _entities.Remove(entity);

    public void AddSystem(BaseSystem system)
    {
        _systems.Add(system);
        _systems.Sort((a, b) => a.Priority.CompareTo(b.Priority));
    }

    public void RemoveSystem(BaseSystem system) => _systems.Remove(system);

    public void Update(GameTime gameTime)
    {
        foreach (var system in _systems.Where(s => s.IsEnabled))
        {
            system.Update(gameTime, _entities);
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (var system in _systems.Where(s => s.IsEnabled))
        {
            system.Draw(gameTime, _entities, spriteBatch);
        }
    }
}
