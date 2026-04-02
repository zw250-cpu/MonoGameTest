using Microsoft.Xna.Framework;
using MonoGameTest.Components;
using MonoGameTest.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MonoGameTest.Systems;

public class MovementSystem : BaseSystem
{
    public override void Update(GameTime gameTime, IEnumerable<Entity> entities)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var velocity = entity.GetComponent<VelocityComponent>();

            if (transform != null && velocity != null)
            {
                transform.Translate(velocity.Velocity * deltaTime);
            }
        }
    }
}
