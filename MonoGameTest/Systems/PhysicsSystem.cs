using Microsoft.Xna.Framework;
using MonoGameTest.Components;
using MonoGameTest.Entities;
using System.Collections.Generic;

namespace MonoGameTest.Systems;

public class PhysicsSystem : BaseSystem
{
    public Vector3 Gravity { get; set; } = new Vector3(0, 980f, 0); // Pixels per second squared

    public override void Update(GameTime gameTime, IEnumerable<Entity> entities)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var rb = entity.GetComponent<RigidbodyComponent>();

            if (transform != null && rb != null && !rb.IsKinematic)
            {
                // Apply Gravity
                if (rb.UseGravity)
                {
                    rb.Velocity += Gravity * deltaTime;
                }

                // Apply Drag (simple linear dampening)
                if (rb.Drag > 0)
                {
                    rb.Velocity *= MathHelper.Clamp(1.0f - (rb.Drag * deltaTime), 0, 1);
                }

                // Update Position
                transform.Position += rb.Velocity * deltaTime;
            }
        }
    }
}
