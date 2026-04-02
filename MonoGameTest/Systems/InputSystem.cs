using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameTest.Components;
using MonoGameTest.Entities;
using System;
using System.Collections.Generic;

namespace MonoGameTest.Systems;

public class InputSystem : BaseSystem
{
    public override void Update(GameTime gameTime, IEnumerable<Entity> entities)
    {
        var keyboardState = Keyboard.GetState();

        foreach (var entity in entities)
        {
            var input = entity.GetComponent<InputComponent>();
            var rb = entity.GetComponent<RigidbodyComponent>();

            if (input != null && rb != null)
            {
                float horizontal = 0;
                if (keyboardState.IsKeyDown(input.LeftKey)) horizontal -= 1;
                if (keyboardState.IsKeyDown(input.RightKey)) horizontal += 1;

                // Update horizontal velocity (preserving current Y)
                rb.Velocity = new Vector3(horizontal * input.MoveSpeed, rb.Velocity.Y, 0);

                // Simple Jump (Only if not falling/jumping already)
                if (keyboardState.IsKeyDown(input.JumpKey) && Math.Abs(rb.Velocity.Y) < 0.1f)
                {
                    rb.Velocity = new Vector3(rb.Velocity.X, -input.JumpForce, 0);
                }
            }
        }
    }
}
