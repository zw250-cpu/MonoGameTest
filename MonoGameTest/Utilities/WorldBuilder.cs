using MonoGameTest.Systems;

namespace MonoGameTest.Utilities;

public class WorldBuilder
{
    private readonly World _world;

    public WorldBuilder()
    {
        _world = new World();
    }

    /// <summary>
    /// Adds a system to the world being built.
    /// </summary>
    public WorldBuilder AddSystem(BaseSystem system)
    {
        _world.AddSystem(system);
        return this;
    }

    /// <summary>
    /// Adds all the standard systems (Input, Physics, Collision, SpriteRender) with default priorities.
    /// </summary>
    public WorldBuilder AddDefaultSystems()
    {
        _world.AddSystem(new InputSystem { Priority = 0 });
        _world.AddSystem(new PhysicsSystem { Priority = 1 });
        _world.AddSystem(new CollisionSystem { Priority = 2 });
        _world.AddSystem(new CameraFollowSystem { Priority = 3 });
        _world.AddSystem(new SpriteRenderSystem { Priority = 10 });
        return this;
    }

    /// <summary>
    /// Finalizes the world creation.
    /// </summary>
    public World Build()
    {
        return _world;
    }
}
