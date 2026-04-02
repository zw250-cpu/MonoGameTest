using Microsoft.Xna.Framework;
using MonoGameTest.Components;
using MonoGameTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoGameTest.Systems;

public class CollisionSystem : BaseSystem
{
    public override void Update(GameTime gameTime, IEnumerable<Entity> entities)
    {
        var colliders = entities
            .Where(e => e.HasComponent<BoxCollider2DComponent>())
            .ToList();

        for (int i = 0; i < colliders.Count; i++)
        {
            for (int j = i + 1; j < colliders.Count; j++)
            {
                var entityA = colliders[i];
                var entityB = colliders[j];

                if (Intersects(entityA, entityB, out Vector2 overlap))
                {
                    var colliderA = entityA.GetComponent<BoxCollider2DComponent>();
                    var colliderB = entityB.GetComponent<BoxCollider2DComponent>();

                    if (!colliderA.IsTrigger && !colliderB.IsTrigger)
                    {
                        ResolveCollision(entityA, entityB, overlap);
                    }
                }
            }
        }
    }

    private bool Intersects(Entity a, Entity b, out Vector2 overlap)
    {
        overlap = Vector2.Zero;

        var colA = a.GetComponent<BoxCollider2DComponent>();
        var colB = b.GetComponent<BoxCollider2DComponent>();

        float aWidth = colA.Size.X * a.Transform.Scale.X;
        float aHeight = colA.Size.Y * a.Transform.Scale.Y;
        float aX = a.Transform.Position.X + (colA.Offset.X * a.Transform.Scale.X) - (aWidth / 2f);
        float aY = a.Transform.Position.Y + (colA.Offset.Y * a.Transform.Scale.Y) - (aHeight / 2f);

        float bWidth = colB.Size.X * b.Transform.Scale.X;
        float bHeight = colB.Size.Y * b.Transform.Scale.Y;
        float bX = b.Transform.Position.X + (colB.Offset.X * b.Transform.Scale.X) - (bWidth / 2f);
        float bY = b.Transform.Position.Y + (colB.Offset.Y * b.Transform.Scale.Y) - (bHeight / 2f);

        float dx = (aX + aWidth / 2f) - (bX + bWidth / 2f);
        float dy = (aY + aHeight / 2f) - (bY + bHeight / 2f);
        float combinedHalfWidths = (aWidth + bWidth) / 2f;
        float combinedHalfHeights = (aHeight + bHeight) / 2f;

        if (Math.Abs(dx) < combinedHalfWidths && Math.Abs(dy) < combinedHalfHeights)
        {
            float overlapX = combinedHalfWidths - Math.Abs(dx);
            float overlapY = combinedHalfHeights - Math.Abs(dy);
            overlap = new Vector2(overlapX, overlapY);
            return true;
        }

        return false;
    }

    private void ResolveCollision(Entity a, Entity b, Vector2 overlap)
    {
        var rbA = a.GetComponent<RigidbodyComponent>();
        var rbB = b.GetComponent<RigidbodyComponent>();

        // Resolve on the axis with the smallest overlap
        if (overlap.X < overlap.Y)
        {
            float push = overlap.X;
            if (a.Transform.Position.X < b.Transform.Position.X) push = -push;

            if (rbA != null && !rbA.IsKinematic)
            {
                a.Transform.Position += new Vector3(push, 0, 0);
                rbA.Velocity = new Vector3(0, rbA.Velocity.Y, 0);
            }
            if (rbB != null && !rbB.IsKinematic)
            {
                b.Transform.Position -= new Vector3(push, 0, 0);
                rbB.Velocity = new Vector3(0, rbB.Velocity.Y, 0);
            }
        }
        else
        {
            float push = overlap.Y;
            if (a.Transform.Position.Y < b.Transform.Position.Y) push = -push;

            if (rbA != null && !rbA.IsKinematic)
            {
                a.Transform.Position += new Vector3(0, push, 0);
                rbA.Velocity = new Vector3(rbA.Velocity.X, 0, 0);
            }
            if (rbB != null && !rbB.IsKinematic)
            {
                b.Transform.Position -= new Vector3(0, push, 0);
                rbB.Velocity = new Vector3(rbB.Velocity.X, 0, 0);
            }
        }
    }
}
